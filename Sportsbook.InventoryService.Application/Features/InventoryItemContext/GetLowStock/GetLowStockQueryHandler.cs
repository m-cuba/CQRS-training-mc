using Sportsbook.InventoryService.Application.Seedwork.Interfaces;
using Sportsbook.InventoryService.Application.Seedwork.Responses;
using Sportsbook.InventoryService.Core.InventoryContext.Repositories;

namespace Sportsbook.InventoryService.Application.Features.InventoryItemContext.GetLowStock
{
    public class GetLowStockQueryHandler(IInventoryReadRepository readRepository) : IGetLowStockItemsQueryHandler
    {
        public async Task<List<InventoryItemResponse>> Handle(int threshold = 10, CancellationToken cancellationToken = default)
        {
            if (threshold > 100)
            {
                throw new ArgumentOutOfRangeException(nameof(threshold), "Threshold must be between 0 and 100.");
            }

            var items = await readRepository.GetLowStockAsync(threshold, cancellationToken);

            return [.. items.Select(i => new InventoryItemResponse(i.Sku.Value, i.Name, i.Quantity.Value))];
        }
    }
}