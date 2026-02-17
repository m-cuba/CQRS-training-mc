using Microsoft.EntityFrameworkCore;
using Sportsbook.InventoryService.Core.InventoryContext;
using Sportsbook.InventoryService.Core.InventoryContext.Repositories;

namespace Sportsbook.InventoryService.Infrastructure.Repositories
{
    public class InventoryReadRepository(ReadDbContext readDb) : IInventoryReadRepository
    {
        private readonly ReadDbContext _readDb = readDb;

        public async Task<InventoryItem?> GetBySkuAsync(Sku sku, CancellationToken cancellationToken = default)
        {
            return await _readDb.InventoryItems
                .SingleOrDefaultAsync(x => x.Sku == sku, cancellationToken);
        }

        public async Task<List<InventoryItem>> GetLowStockAsync(int threshold, CancellationToken cancellationToken = default)
        {
            return await _readDb.InventoryItems
                .AsNoTracking()
                .Where(i => i.Quantity.Value < threshold)
                .OrderBy(i => i.Quantity.Value)
                .ToListAsync(cancellationToken);
        }

        public async Task<List<InventoryItem>> GetAllAsync(CancellationToken cancellationToken = default)
        {
            return await _readDb.InventoryItems
                .AsNoTracking()
                .ToListAsync(cancellationToken);
        }

        public Task AddAsync(InventoryItem item, CancellationToken cancellationToken = default)
        {
            _readDb.InventoryItems.Add(item);
            return Task.CompletedTask;
        }

        public Task AddStockMovementAuditAsync(StockMovement stockMovement, CancellationToken cancellationToken = default)
        {
            _readDb.StockMovements.Add(stockMovement);

            return Task.CompletedTask;
        }
    }
}