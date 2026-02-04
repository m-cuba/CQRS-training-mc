using Sportsbook.ServiceTemplate.Core.Entities;
using Sportsbook.ServiceTemplate.Core.ValueObjects;

namespace Sportsbook.ServiceTemplate.Application.Interfaces
{
    public interface IInventoryRepository
    {
        Task<InventoryItem?> GetBySkuAsync(Sku sku, CancellationToken cancellationToken = default);
        Task<List<InventoryItem>> GetAllAsync(CancellationToken cancellationToken = default);
        Task AddAsync(InventoryItem item, CancellationToken cancellationToken = default);
        Task UpdateAsync(InventoryItem item, CancellationToken cancellationToken = default);
    }
}
