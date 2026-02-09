using Sportsbook.InventoryService.Core.Entities;
using Sportsbook.InventoryService.Core.ValueObjects;

namespace Sportsbook.InventoryService.Core.Repositories
{
    public interface IInventoryRepository
    {
        Task<InventoryItem?> GetBySkuAsync(Sku sku, CancellationToken cancellationToken = default);
        Task<List<InventoryItem>> GetAllAsync(CancellationToken cancellationToken = default);
        Task AddAsync(InventoryItem item, CancellationToken cancellationToken = default);
    }
}
