using Microsoft.EntityFrameworkCore;
using Sportsbook.ServiceTemplate.Application.Interfaces;
using Sportsbook.ServiceTemplate.Core.Entities;
using Sportsbook.ServiceTemplate.Core.ValueObjects;

namespace Sportsbook.ServiceTemplate.Infrastructure.Repositories
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

        public async Task AddAsync(InventoryItem item, CancellationToken cancellationToken = default)
        {
            _db.InventoryItems.Add(item);
            await _db.SaveChangesAsync(cancellationToken);
        }

        public async Task UpdateAsync(InventoryItem item, CancellationToken cancellationToken = default)
        {
            _db.InventoryItems.Update(item);
            await _db.SaveChangesAsync(cancellationToken);
        }
    }
}
