using Sportsbook.InventoryService.Application.Seedwork.Responses;

namespace Sportsbook.InventoryService.Application.Seedwork.Interfaces
{
    public interface IGetAllItemsQueryHandler
    {
        Task<List<InventoryItemResponse>> Handle(CancellationToken cancellationToken = default);
    }
}
