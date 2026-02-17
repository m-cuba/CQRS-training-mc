using Sportsbook.InventoryService.Application.Seedwork.Interfaces;
using Sportsbook.InventoryService.Application.Seedwork.Responses;
using Sportsbook.InventoryService.Core.InventoryContext;
using Sportsbook.InventoryService.Core.InventoryContext.Repositories;

namespace Sportsbook.InventoryService.Application.Features.InventoryItemContext.GetBySku
{
    public class GetBySkuQueryHandler(IInventoryReadRepository repository) : IGetItemBySkuQueryHandler
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
