namespace Sportsbook.InventoryService.Infrastructure.Messaging
{
    public class EventStoreMessage
    {
        public Guid Id { get; set; }

        public string StreamId { get; set; } = default!;

        public string EventType { get; set; } = default!;

        public string Payload { get; set; } = default!;

        public DateTimeOffset OccurredAt { get; set; }

        public int Version { get; set; }
    }
}
