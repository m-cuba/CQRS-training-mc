namespace InventoryService.Audit.Core
{
    public interface IAuditStockRepository
    {
        Task<bool> ExistsAsync(Guid movementId, CancellationToken ct);
        Task AddAsync(AuditStockEntry entry, CancellationToken ct);
        Task<int> GetStockAtAsync(string sku, DateTimeOffset date, CancellationToken ct);
    }
}
