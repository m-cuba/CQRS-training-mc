using Sportsbook.InventoryService.Core.InventoryContext.Events;

namespace Sportsbook.InventoryService.Application.Seedwork.Interfaces
{
    public interface IStockChangedHandler
    {
        Task Handle(StockChangedEvent @event, CancellationToken cancellationToken = default);
    }
}
