using Sportsbook.ServiceTemplate.Application.DTOs;
using Sportsbook.ServiceTemplate.Application.Interfaces;
using Sportsbook.ServiceTemplate.Core.ValueObjects;

namespace Sportsbook.ServiceTemplate.Application.Queries.GetItemBySku
{
    public class GetItemBySkuQueryHandler(IInventoryRepository repository) : IGetItemBySkuQueryHandler
    {
        public async Task<InventoryItemDto?> Handle(GetItemBySkuQuery query, CancellationToken cancellationToken = default)
        {
            var item = await repository.GetBySkuAsync(new Sku(query.Sku));

            return item is null
                ? null
                : new InventoryItemDto(
                    item.Sku.Value,
                    item.Name,
                    item.Quantity
                  );
        }
    }

}
