using InventoryService.Contracts;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Sportsbook.InventoryService.Core.InventoryContext.Repositories;
using Sportsbook.InventoryService.Infrastructure.Messaging;
using System.Text.Json;

namespace Sportsbook.InventoryService.Infrastructure.Repositories
{
    public sealed class EventStoreRepository(InventoryDbContext context, ILogger<EventStoreRepository> logger) : IInventoryEventStoreRepository
    {
        public async Task<IReadOnlyCollection<IDomainEvent>> LoadStreamAsync(string streamId, CancellationToken ct)
        {
            var records = await context.EventStore
                .AsNoTracking()
                .Where(x => x.StreamId == streamId)
                .OrderBy(x => x.Version)
                .ToListAsync(ct);

            var events = new List<IDomainEvent>();

            foreach (var record in records)
            {
                logger.LogInformation("Loading event -> Type: {Type}, Payload: {Payload}",
                    record.EventType,
                    record.Payload);

                var type = DomainEventTypeResolver.Resolve(record.EventType);

                var domainEvent = (IDomainEvent)JsonSerializer.Deserialize(
                    record.Payload,
                    type)!;

                logger.LogInformation("Deserialized event -> {@Event}", domainEvent);

                events.Add(domainEvent);
            }

            return events;
        }

        public async Task<int> GetNextVersionAsync(string streamId, CancellationToken ct)
        {
            var last = await context.EventStore
                .Where(x => x.StreamId == streamId)
                .OrderByDescending(x => x.Version)
                .Select(x => (int?)x.Version)
                .FirstOrDefaultAsync(ct);

            return (last ?? 0) + 1;
        }

        public void Append(string streamId, IDomainEvent @event, int version)
        {
            var message = new EventStoreMessage
            {
                Id = Guid.NewGuid(),
                StreamId = streamId,
                EventType = @event.GetType().Name,
                Payload = JsonSerializer.Serialize(@event),
                OccurredAt = @event.OccurredAt,
                Version = version
            };

            logger.LogInformation("Persisting event -> Type: {Type}, Payload: {Payload}",
                @event.GetType().Name,
                message.Payload);

            context.EventStore.Add(message);
        }
    }
}
