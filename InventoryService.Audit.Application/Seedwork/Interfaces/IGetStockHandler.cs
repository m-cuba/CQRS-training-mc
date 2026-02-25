namespace InventoryService.Audit.Application.Seedwork.Interfaces
{
    public interface IGetStockHandler
    {
        Task<int> HandleAsync(string sku, DateTimeOffset date, CancellationToken ct);
    }
}
