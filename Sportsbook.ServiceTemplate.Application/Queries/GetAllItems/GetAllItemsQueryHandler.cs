using Sportsbook.ServiceTemplate.Application.DTOs;
using Sportsbook.ServiceTemplate.Application.Interfaces;

namespace Sportsbook.ServiceTemplate.Application.Queries.GetAllItems
{
    public class GetAllItemsQueryHandler(IInventoryRepository repository) : IGetAllItemsQueryHandler
    {
        public async Task<List<InventoryItemDto>> Handle(CancellationToken cancellationToken = default)
        {
            var items = await repository.GetAllAsync();

            return [.. items
                .Select(x => new InventoryItemDto(
                    x.Sku.Value,
                    x.Name,
                    x.Quantity))];
        }
    }
}
