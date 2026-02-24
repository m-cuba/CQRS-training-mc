using DotPulsar.Abstractions;
using InventoryService.Audit.Application.Seedwork.Interfaces;
using InventoryService.Contracts;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System.Buffers;
using System.Text;
using System.Text.Json;

namespace InventoryService.Audit.Infrastructure.Consumers
{
    public sealed class StockChangedConsumer(
        IConsumer<ReadOnlySequence<byte>> consumer,
        IServiceScopeFactory scopeFactory,
        ILogger<StockChangedConsumer> logger) : BackgroundService
    {
        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            logger.LogInformation("StockChangedConsumer starting");

            while (!stoppingToken.IsCancellationRequested)
            {
                try
                {
                    var message = await consumer.Receive(stoppingToken);

                    ReadOnlySequence<byte> dataSeq = message.Data;
                    var payload = dataSeq.ToArray();
                    var json = Encoding.UTF8.GetString(payload);

                    using var doc = JsonDocument.Parse(json);
                    var root = doc.RootElement;

                    // Expect CloudEvent structured-mode JSON with "data"
                    if (!root.TryGetProperty("data", out var dataElement))
                    {
                        logger.LogWarning("CloudEvent does not contain 'data' property. Acking message.");
                        await consumer.Acknowledge(message.MessageId, stoppingToken);
                        continue;
                    }

                    var dataJson = dataElement.GetRawText();
                    logger.LogInformation("CloudEvent data payload: {DataJson}", dataJson);

                    var stockChangedEvent = JsonSerializer.Deserialize<StockChangedEvent>(dataJson);

                    if (stockChangedEvent is null)
                    {
                        logger.LogWarning("Failed to deserialize StockChangedEvent; acknowledging to avoid replay.");
                        await consumer.Acknowledge(message.MessageId, stoppingToken);
                        continue;
                    }

                    logger.LogInformation(
                        "Deserialized StockChangedEvent -> Sku: {Sku}, Qty: {Qty}, MovementId: {MovementId}, OccurredAt: {OccurredAt}",
                        stockChangedEvent.Sku,
                        stockChangedEvent.Quantity,
                        stockChangedEvent.MovementId,
                        stockChangedEvent.OccurredAt);

                    using var scope = scopeFactory.CreateScope();
                    var handler = scope.ServiceProvider.GetRequiredService<IStockChangedAuditHandler>();

                    await handler.HandleAsync(stockChangedEvent, stoppingToken);

                    await consumer.Acknowledge(message.MessageId, stoppingToken);
                }
                catch (Exception ex)
                {
                    logger.LogError(ex, "Error processing audit event");
                }
            }
        }
    }
}
