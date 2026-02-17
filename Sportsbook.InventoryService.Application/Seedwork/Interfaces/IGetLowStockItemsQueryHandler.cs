using Sportsbook.InventoryService.Application.Seedwork.Responses;

namespace Sportsbook.InventoryService.Application.Seedwork.Interfaces
{
    public interface IGetLowStockItemsQueryHandler
    {
        Task<List<InventoryItemResponse>> Handle(int threshold = 10, CancellationToken cancellationToken = default);
    }
}