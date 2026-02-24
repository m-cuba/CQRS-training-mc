using FluentValidation;
using Microsoft.Extensions.Logging;
using Sportsbook.InventoryService.Application.Seedwork.Interfaces;
using Sportsbook.InventoryService.Core.InventoryContext;
using Sportsbook.InventoryService.Core.InventoryContext.Repositories;

namespace Sportsbook.InventoryService.Application.Features.InventoryItemContext.RemoveStock
{
    public class RemoveStockCommandHandler(
        IInventoryEventStoreRepository eventStoreRepository,
        IUnitOfWork unitOfWork,
        IValidator<RemoveStockCommand> validator,
        IEventPublisher eventPublisher,
        ILogger<RemoveStockCommandHandler> logger) : IRemoveStockCommandHandler
    {
        public async Task Handle(RemoveStockCommand command, CancellationToken cancellationToken = default)
        {
            await validator.ValidateAndThrowAsync(command, cancellationToken);

            var streamId = $"inventory-item-{command.Sku}";
            var history = await eventStoreRepository.LoadStreamAsync(streamId, cancellationToken);

            if (history.Count == 0)
            {
                throw new InvalidOperationException("Item not found");
            }
            logger.LogInformation("history count {Count}", history.Count);

            var aggregate = InventoryItemAggregate.Rehydrate(history);

            aggregate.RemoveStock(command.Quantity);

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
