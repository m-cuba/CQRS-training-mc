namespace Sportsbook.InventoryService.Core.InventoryContext.Repositories
{
    public interface IInventoryRepository
    {
        Task<InventoryItem?> GetBySkuAsync(Sku sku, CancellationToken cancellationToken = default);
        Task AddAsync(InventoryItem item, CancellationToken cancellationToken = default);
    }
}
