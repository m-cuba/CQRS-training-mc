namespace Sportsbook.InventoryService.Core.InventoryContext
{
    public class InsufficientStockException(Sku sku) : Exception($"Insufficient stock for item with SKU '{sku.Value}'.")
    {
        public Sku Sku { get; } = sku;
    }
}
