using InventoryService.Contracts;

namespace Sportsbook.InventoryService.Core.InventoryContext
{
    public class InventoryItemAggregate
    {
        private readonly List<StockChangedEvent> _uncommittedEvents = [];

        public string Sku { get; private set; } = default!;
        public string Name { get; private set; } = default!;
        public int Stock { get; private set; }

        public IReadOnlyCollection<StockChangedEvent> UncommittedEvents => _uncommittedEvents;

        private InventoryItemAggregate() { }

        public static InventoryItemAggregate Rehydrate(IEnumerable<IDomainEvent> events)
        {
            var aggregate = new InventoryItemAggregate();

            foreach (var e in events)
            {
                aggregate.ApplyState(e);
            }

            return aggregate;
        }

        public void AddStock(int quantity)
        {
            if (quantity <= 0) throw new InvalidOperationException();

            var ev = new StockChangedEvent(Sku, Name, quantity, DateTime.UtcNow, Guid.NewGuid());

            ApplyState(ev);
            _uncommittedEvents.Add(ev);
        }

        public void RemoveStock(int quantity)
        {
            if (Stock - quantity < 0)
                throw new InvalidOperationException($"Stock cannot be negative. Stock {Stock} quantity {quantity}");

            var ev = new StockChangedEvent(Sku, Name, -quantity, DateTime.UtcNow, Guid.NewGuid());

            ApplyState(ev);
            _uncommittedEvents.Add(ev);
        }

        private void ApplyState(IDomainEvent @event)
        {
            switch (@event)
            {
                case ItemCreatedEvent created:
                    Sku = created.Sku;
                    Name = created.Name;
                    break;

                case StockChangedEvent changed:
                    Stock += changed.Quantity;
                    Sku = changed.Sku;
                    Name = changed.Name;
                    break;
            }
        }
    }
}
