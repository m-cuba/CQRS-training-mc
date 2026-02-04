using Sportsbook.ServiceTemplate.Application.DTOs;
using Sportsbook.ServiceTemplate.Application.Queries.GetItemBySku;

namespace Sportsbook.ServiceTemplate.Application.Interfaces
{
    public interface IGetItemBySkuQueryHandler
    {
        Task<InventoryItemDto?> Handle(GetItemBySkuQuery query, CancellationToken cancellationToken = default);
    }
}
