using DotPulsar.Abstractions;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Sportsbook.InventoryService.Application.Seedwork.Interfaces;
using Sportsbook.InventoryService.Application.Seedwork.Services;
using Sportsbook.InventoryService.Core.InventoryContext.Events;
using System.Buffers;
using System.Text;
using System.Text.Json;

namespace Sportsbook.InventoryService.Infrastructure.Messaging
{
    public sealed class EventConsumer(
        IConsumer<ReadOnlySequence<byte>> consumer,
        IServiceScopeFactory scopeFactory,
        ILogger<EventConsumer> logger) : BackgroundService
    {
        protected override async Task ExecuteAsync(CancellationToken cancellationToken)
        {
            logger.LogInformation("EventConsumer starting");

            while (!cancellationToken.IsCancellationRequested)
            {
                try
                {
                    var message = await consumer.Receive(cancellationToken);
                    ReadOnlySequence<byte> dataSeq = message.Data;
                    var payload = dataSeq.ToArray();
                    var json = Encoding.UTF8.GetString(payload);

                    using var doc = JsonDocument.Parse(json);
                    var root = doc.RootElement;

                    // Expect CloudEvent structured-mode JSON with "data"
                    var dataElement = root.GetProperty("data");
                    var stockChangedEvent = JsonSerializer.Deserialize<StockChangedEvent>(dataElement.GetRawText());

                    if (stockChangedEvent is null)
                    {
                        logger.LogWarning("Failed to deserialize StockChangedEvent; acknowledging to avoid replay.");
                        await consumer.Acknowledge(message.MessageId, cancellationToken);
                        continue;
                    }

                    using var scope = scopeFactory.CreateScope();
                    var stockMovementService = scope.ServiceProvider.GetRequiredService<IStockMovementService>();

                    if (await stockMovementService.ExistsAsync(stockChangedEvent.MovementId))
                    {
                        logger.LogInformation("Event with MovementId {MovementId} was already consumed", stockChangedEvent.MovementId);
                        await consumer.Acknowledge(message.MessageId, cancellationToken);
                        continue;
                    }

                    var handler = scope.ServiceProvider.GetRequiredService<IStockChangedHandler>();

                    await handler.Handle(stockChangedEvent, cancellationToken);

                    await consumer.Acknowledge(message.MessageId, cancellationToken);
                }
                catch (OperationCanceledException)
                {
                    break;
                }
                catch (Exception ex)
                {
                    logger.LogError(ex, "Error processing message; message left unacknowledged");
                }
            }
        }
    }
}