namespace Sportsbook.InventoryService.Core.InventoryContext
{
    public interface IInventoryRepository
    {
        Task<InventoryItem?> GetBySkuAsync(Sku sku, CancellationToken cancellationToken = default);
        Task<List<InventoryItem>> GetAllAsync(CancellationToken cancellationToken = default);
        Task AddAsync(InventoryItem item, CancellationToken cancellationToken = default);
    }
}
