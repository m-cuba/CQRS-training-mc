using InventoryService.Contracts;

namespace InventoryService.Audit.Application.Seedwork.Interfaces
{
    public interface IStockChangedAuditHandler
    {
        Task HandleAsync(StockChangedEvent @event, CancellationToken ct);
    }
}
