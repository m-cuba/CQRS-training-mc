using InventoryService.Audit.Application.Seedwork.Interfaces;
using InventoryService.Audit.Core;

namespace InventoryService.Audit.Application.Features.GetStock
{
    public class GetStockHandler (IAuditStockRepository repository) : IGetStockHandler
    {
        public async Task<int> HandleAsync(string sku, DateTimeOffset date, CancellationToken ct)
        {
            return await repository.GetStockAtAsync(sku, date, ct);
        }
    }
}
