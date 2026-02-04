using Sportsbook.ServiceTemplate.Application.DTOs;

namespace Sportsbook.ServiceTemplate.Application.Interfaces
{
    public interface IGetAllItemsQueryHandler
    {
        Task<List<InventoryItemDto>> Handle(CancellationToken cancellationToken = default);
    }
}
