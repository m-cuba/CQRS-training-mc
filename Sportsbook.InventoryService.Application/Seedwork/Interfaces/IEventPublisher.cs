namespace Sportsbook.InventoryService.Application.Seedwork.Interfaces
{
    public interface IEventPublisher
    {
        Task PublishAsync<TEvent>(
            TEvent eventToPublish,
            CancellationToken cancellationToken = default);
    }
}
