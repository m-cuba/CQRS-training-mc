using Sportsbook.InventoryService.Core.InventoryContext.Repositories;

namespace Sportsbook.InventoryService.Application.Seedwork.Services
{
    internal class StockMovementService (IStockMovementReadRepository stockMovementReadRepository) : IStockMovementService
    {
        public async Task<bool> ExistsAsync(Guid guid)
        {
            return await stockMovementReadRepository.ExistsAsync(guid);
        }
    }
}
