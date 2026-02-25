using InventoryService.Contracts;

namespace Sportsbook.InventoryService.Core.InventoryContext.Repositories
{
    public interface IInventoryEventStoreRepository
    {
        Task<IReadOnlyCollection<IDomainEvent>> LoadStreamAsync(string streamId, CancellationToken ct);

        Task<int> GetNextVersionAsync(string streamId, CancellationToken ct);

        void Append(string streamId, IDomainEvent @event, int version);
    }
}
