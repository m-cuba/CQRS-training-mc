using Sportsbook.InventoryService.Application.Features.InventoryItemContext.GetBySku;
using Sportsbook.InventoryService.Application.Seedwork.Responses;

namespace Sportsbook.InventoryService.Application.Seedwork.Interfaces
{
    public interface IGetItemBySkuQueryHandler
    {
        Task<InventoryItemResponse?> Handle(GetBySkuQuery query, CancellationToken cancellationToken = default);
    }
}
