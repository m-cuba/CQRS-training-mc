using FluentValidation;
using Sportsbook.InventoryService.Application.Seedwork.Interfaces;
using Sportsbook.InventoryService.Core.InventoryContext;
using Sportsbook.InventoryService.Core.InventoryContext.Repositories;

namespace Sportsbook.InventoryService.Application.Features.InventoryItemContext.AddStock
{
    public class AddStockCommandHandler(
        IInventoryEventStoreRepository eventStoreRepository,
        IUnitOfWork unitOfWork,
        IValidator<AddStockCommand> validator,
        IEventPublisher eventPublisher) : IAddStockCommandHandler
    {
        public async Task Handle(AddStockCommand command, CancellationToken cancellationToken = default)
        {
            await validator.ValidateAndThrowAsync(command, cancellationToken);

            var streamId = $"inventory-item-{command.Sku}";
            var history = await eventStoreRepository.LoadStreamAsync(streamId, cancellationToken);

            if (history.Count == 0)
            {
                throw new InvalidOperationException("Item not found");
            }

            var aggregate = InventoryItemAggregate.Rehydrate(history);

            aggregate.AddStock(command.Quantity);

            foreach (var @event in aggregate.UncommittedEvents)
            {
                var version = await eventStoreRepository.GetNextVersionAsync(streamId, cancellationToken);
                eventStoreRepository.Append(streamId, @event, version);

                await eventPublisher.PublishAsync(@event, cancellationToken);
            }

            await unitOfWork.SaveChangesAsync(cancellationToken);
        }
    }
}
