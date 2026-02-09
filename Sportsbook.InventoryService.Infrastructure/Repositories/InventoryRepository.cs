using Microsoft.EntityFrameworkCore;
using Sportsbook.InventoryService.Core.InventoryContext;

namespace Sportsbook.InventoryService.Infrastructure.Repositories
{
    public class InventoryRepository(InventoryDbContext db) : IInventoryRepository
    {
        private readonly InventoryDbContext _db = db;

        public async Task<InventoryItem?> GetBySkuAsync(Sku sku, CancellationToken cancellationToken = default)
        {
            return await _db.InventoryItems
                .SingleOrDefaultAsync(x => x.Sku == sku, cancellationToken);
        }

        public async Task<List<InventoryItem>> GetAllAsync(CancellationToken cancellationToken = default)
        {
            return await _db.InventoryItems.ToListAsync(cancellationToken);
        }

        public Task AddAsync(InventoryItem item, CancellationToken cancellationToken = default)
        {
            _db.InventoryItems.Add(item);
            return Task.CompletedTask;
        }

        public Task UpdateAsync(InventoryItem item, CancellationToken cancellationToken = default)
        {
            _db.InventoryItems.Update(item);
            return Task.CompletedTask;
        }
    }
}
