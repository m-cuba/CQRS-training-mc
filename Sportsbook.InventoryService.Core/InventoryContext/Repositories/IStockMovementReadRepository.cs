namespace Sportsbook.InventoryService.Core.InventoryContext.Repositories
{
    public interface IStockMovementReadRepository
    {
        Task<List<string>> GetAuditAsync(string sku, CancellationToken cancellationToken = default);

        Task<bool> ExistsAsync(Guid id, CancellationToken cancellationToken = default);
    }
}
