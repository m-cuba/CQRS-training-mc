using Sportsbook.InventoryService.Application.Seedwork.Interfaces;
using Sportsbook.InventoryService.Application.Seedwork.Responses;
using Sportsbook.InventoryService.Core.InventoryContext;

namespace Sportsbook.InventoryService.Application.Features.InventoryItemContext.GetBySku
{
    public class GetBySkuQueryHandler(IInventoryRepository repository) : IGetItemBySkuQueryHandler
    {
        public async Task<InventoryItemResponse?> Handle(GetBySkuQuery query, CancellationToken cancellationToken = default)
        {
            var item = await repository.GetBySkuAsync(new Sku(query.Sku), cancellationToken);

            return item is null
                ? null
                : new InventoryItemResponse(
                    item.Sku.Value,
                    item.Name,
                    item.Quantity.Value
                  );
        }
    }

}
