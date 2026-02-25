using InventoryService.Contracts;

namespace Sportsbook.InventoryService.Infrastructure.Messaging
{
    internal static class DomainEventTypeResolver
    {
        private static readonly Dictionary<string, Type> Types = new()
    {
        { nameof(ItemCreatedEvent), typeof(ItemCreatedEvent) },
        { nameof(StockChangedEvent), typeof(StockChangedEvent) }
    };

        public static Type Resolve(string eventType)
        {
            if (!Types.TryGetValue(eventType, out var type))
                throw new InvalidOperationException($"Unknown event type: {eventType}");

            return type;
        }
    }
}
