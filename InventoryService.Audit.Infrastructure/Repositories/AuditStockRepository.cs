using InventoryService.Audit.Core;
using InventoryService.Audit.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace InventoryService.Audit.Infrastructure.Repositories
{
    public class AuditStockRepository(AuditDbContext db) : IAuditStockRepository
    {
        public async Task<bool> ExistsAsync(Guid movementId, CancellationToken ct)
            => await db.AuditEntries.AnyAsync(x => x.MovementId == movementId, ct);

        public async Task AddAsync(AuditStockEntry entry, CancellationToken ct)
            => await db.AuditEntries.AddAsync(entry, ct);

        public async Task<int> GetStockAtAsync(string sku, DateTimeOffset date, CancellationToken ct)
        {
            return await db.AuditEntries
                .Where(x => x.Sku == sku && x.OccurredOn <= date)
                .SumAsync(x => (int?)x.QuantityDelta, ct) ?? 0;
        }
    }
}
