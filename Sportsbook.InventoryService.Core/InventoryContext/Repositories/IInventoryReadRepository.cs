namespace Sportsbook.InventoryService.Core.InventoryContext.Repositories
{
    public interface IInventoryReadRepository
    {
        Task<List<InventoryItem>> GetLowStockAsync(int threshold, CancellationToken cancellationToken = default);
        
        Task AddAsync(InventoryItem item, CancellationToken cancellationToken = default);

        Task<InventoryItem?> GetBySkuAsync(Sku sku, CancellationToken cancellationToken = default);

        Task<List<InventoryItem>> GetAllAsync(CancellationToken cancellationToken = default);

        Task AddStockMovementAuditAsync(StockMovement stockMovement, CancellationToken cancellationToken = default);
    }
}