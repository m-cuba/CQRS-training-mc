using Sportsbook.InventoryService.Application.Seedwork.Interfaces;
using Sportsbook.InventoryService.Core.InventoryContext;
using Sportsbook.InventoryService.Core.InventoryContext.Events;
using Sportsbook.InventoryService.Core.InventoryContext.Repositories;

namespace Sportsbook.InventoryService.Application.Features.InventoryItemContext.StockChanged
{
    internal sealed class StockChangedHandler(
        IInventoryReadRepository readRepository,
        IReadUnitOfWork unitOfWork) : IStockChangedHandler
    {
        public async Task Handle(StockChangedEvent @event, CancellationToken cancellationToken = default)
        {
            var item = await readRepository.GetBySkuAsync(new Sku(@event.Sku), cancellationToken);

            if (item is null)
            {
                item = new InventoryItem(
                    new Sku(@event.Sku),
                    @event.Name
                );

                await readRepository.AddAsync(item, cancellationToken);
            }

            if (@event.Quantity > 0)
            {
                item.AddStock(new Quantity(@event.Quantity));
            }
            else
            {
                item.RemoveStock(new Quantity(Math.Abs(@event.Quantity)));
            }

            var stockMovement = new StockMovement
            {
                Id = @event.MovementId,
                Sku = @event.Sku,
                QuantityChange = @event.Quantity,
                OccurredAt = @event.OccurredAt
            };

            await readRepository.AddStockMovementAuditAsync(stockMovement, cancellationToken);

            await unitOfWork.SaveChangesAsync(cancellationToken);
        }
    }
}
