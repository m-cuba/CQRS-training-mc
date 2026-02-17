using Sportsbook.InventoryService.Application.Seedwork.Interfaces;
using Sportsbook.InventoryService.Application.Seedwork.Responses;
using Sportsbook.InventoryService.Core.InventoryContext.Repositories;

namespace Sportsbook.InventoryService.Application.Features.InventoryItemContext.GetAll
{
    public class GetAllQueryHandler(IInventoryReadRepository repository) : IGetAllItemsQueryHandler
    {
        public async Task<List<InventoryItemResponse>> Handle(CancellationToken cancellationToken = default)
        {
            var items = await repository.GetAllAsync(cancellationToken);

            return [.. items
                .Select(x => new InventoryItemResponse(
                    x.Sku.Value,
                    x.Name,
                    x.Quantity.Value))];
        }
    }
}
