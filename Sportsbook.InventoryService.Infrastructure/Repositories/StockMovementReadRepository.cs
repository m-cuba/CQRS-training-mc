using Microsoft.EntityFrameworkCore;
using Sportsbook.InventoryService.Core.InventoryContext.Repositories;

namespace Sportsbook.InventoryService.Infrastructure.Repositories
{
    internal class StockMovementReadRepository (ReadDbContext readDb) : IStockMovementReadRepository
    {
        public async Task<List<string>> GetAuditAsync(string sku, CancellationToken cancellationToken = default)
        {
            var rows = await readDb.StockMovements
                .AsNoTracking()
                .Where(a => a.Sku == sku)
                .OrderByDescending(a => a.OccurredAt)
                .Select(a => new { a.Sku, a.QuantityChange, a.OccurredAt })
                .ToListAsync(cancellationToken);

            return [.. rows.Select(a => $"{a.Sku}: {(a.QuantityChange > 0 ? "+" : "")}{a.QuantityChange} @ {a.OccurredAt.ToLocalTime():t}")];
        }

        public async Task<bool> ExistsAsync(Guid id, CancellationToken cancellationToken = default)
        {
            return await readDb.StockMovements
                .AnyAsync(x => x.Id == id, cancellationToken);
        }
    }
}
