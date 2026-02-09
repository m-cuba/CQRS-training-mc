using Sportsbook.InventoryService.Core.ValueObjects;

namespace Sportsbook.InventoryService.Core.Exceptions
{
    public class InsufficientStockException(Sku sku) : Exception($"Insufficient stock for item with SKU '{sku.Value}'.")
    {
        public Sku Sku { get; } = sku;
    }
}
