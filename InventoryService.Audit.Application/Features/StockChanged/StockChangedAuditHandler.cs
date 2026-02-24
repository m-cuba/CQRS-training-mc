using InventoryService.Audit.Application.Seedwork.Interfaces;
using InventoryService.Audit.Core;
using InventoryService.Contracts;
using Microsoft.Extensions.Logging;

namespace InventoryService.Audit.Application.Features.StockChanged
{
    public class StockChangedAuditHandler(
        IAuditStockRepository repository,
        IUnitOfWork unitOfWork,
        ILogger<StockChangedAuditHandler> logger) : IStockChangedAuditHandler
    {
        public async Task HandleAsync(StockChangedEvent @event, CancellationToken ct)
        {
            if (await repository.ExistsAsync(@event.MovementId, ct))
            {
                logger.LogWarning("MovementId {@MovementId} already exists.", @event.MovementId);

                return;
            }

            var entry = new AuditStockEntry(
                Guid.NewGuid(),
                @event.Sku,
                @event.Name,
                @event.Quantity,
                @event.OccurredAt);

            await repository.AddAsync(entry, ct);
            await unitOfWork.SaveChangesAsync(ct);
        }
    }
}
