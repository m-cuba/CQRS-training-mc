namespace Sportsbook.InventoryService.Core.InventoryContext
{
    public class InventoryItem
    {
        public Guid Id { get; private set; }
        public Sku Sku { get; private set; }
        public string Name { get; private set; }
        public Quantity Quantity { get; private set; }

        private InventoryItem() { } // EF Core

        public InventoryItem(Sku sku, string name)
        {
            Id = Guid.NewGuid();
            Sku = sku ?? throw new ArgumentNullException(nameof(sku));
            Name = string.IsNullOrWhiteSpace(name)
                ? throw new ArgumentException("Name cannot be empty")
                : name;

            Quantity = new Quantity(0);
        }

        public void AddStock(Quantity quantity)
        {
            EnsurePositiveMovement(quantity);

            Quantity += quantity;
        }

        public void RemoveStock(Quantity quantity)
        {
            EnsurePositiveMovement(quantity);

            if (Quantity < quantity)
                throw new InsufficientStockException(Sku);

            Quantity -= quantity;
        }

        private static void EnsurePositiveMovement(Quantity quantity)
        {
            if (quantity.IsZero)
                throw new ArgumentException("Quantity must be greater than zero");
        }
    }
}
