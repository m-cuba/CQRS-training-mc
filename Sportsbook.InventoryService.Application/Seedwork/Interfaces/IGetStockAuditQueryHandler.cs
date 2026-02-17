namespace Sportsbook.InventoryService.Application.Seedwork.Interfaces
{
    public interface IGetStockAuditQueryHandler
    {
        Task<List<string>> Handle(string sku, CancellationToken cancellationToken = default);
    }
}