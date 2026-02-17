using Sportsbook.InventoryService.Application.Seedwork.Interfaces;
using Sportsbook.InventoryService.Core.InventoryContext.Repositories;

namespace Sportsbook.InventoryService.Application.Features.InventoryItemContext.GetAudit
{
    public class GetStockAuditQueryHandler(IStockMovementReadRepository readRepository) : IGetStockAuditQueryHandler
    {
        public Task<List<string>> Handle(string sku, CancellationToken cancellationToken = default)
            => readRepository.GetAuditAsync(sku, cancellationToken);
    }
}