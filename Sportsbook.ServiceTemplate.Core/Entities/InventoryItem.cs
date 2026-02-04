using Sportsbook.ServiceTemplate.Core.Exceptions;
using Sportsbook.ServiceTemplate.Core.ValueObjects;

namespace Sportsbook.ServiceTemplate.Core.Entities
{
    public class InventoryItem
    {
        public Guid Id { get; private set; }
        public Sku Sku { get; private set; }
        public string Name { get; private set; }
        public int Quantity { get; private set; }

        private InventoryItem() { } // EF Core

        public InventoryItem(Sku sku, string name)
        {
            Id = Guid.NewGuid();
            Sku = sku ?? throw new ArgumentNullException(nameof(sku));
            Name = string.IsNullOrWhiteSpace(name)
                ? throw new ArgumentException("Name cannot be empty")
                : name;

            Quantity = 0;
        }

        public void AddStock(int quantity)
        {
            if (quantity <= 0)
                throw new ArgumentException("Quantity must be greater than zero");

            Quantity += quantity;
        }

        public void RemoveStock(int quantity)
        {
            if (quantity <= 0)
                throw new ArgumentException("Quantity must be greater than zero");

            if (Quantity < quantity)
                throw new InsufficientStockException(Sku);

            Quantity -= quantity;
        }
    }
}
