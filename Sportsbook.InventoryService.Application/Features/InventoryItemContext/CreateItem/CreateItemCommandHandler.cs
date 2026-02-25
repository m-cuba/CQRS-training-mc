using FluentValidation;
using InventoryService.Contracts;
using Sportsbook.InventoryService.Application.Seedwork.Interfaces;
using Sportsbook.InventoryService.Core.InventoryContext.Repositories;

namespace Sportsbook.InventoryService.Application.Features.InventoryItemContext.CreateItem
{
    public class CreateItemCommandHandler(
        IInventoryEventStoreRepository eventStoreRepository,
        IUnitOfWork unitOfWork,
        IValidator<CreateItemCommand> validator,
        IEventPublisher eventPublisher) : ICreateItemCommandHandler
    {
        public async Task Handle(CreateItemCommand command, CancellationToken cancellationToken = default)
        {
            await validator.ValidateAndThrowAsync(command, cancellationToken);

            var streamId = $"inventory-item-{command.Sku}";
            var history = await eventStoreRepository.LoadStreamAsync(streamId, cancellationToken);

            if (history.Count != 0)
                throw new InvalidOperationException($"Item {command.Sku} already exists");

            var @event = new ItemCreatedEvent(
                command.Sku,
                command.Name,
                DateTimeOffset.UtcNow,
                Guid.NewGuid());

            var version = 1;

            eventStoreRepository.Append(streamId, @event, version);

            await eventPublisher.PublishAsync(@event, cancellationToken);
            await unitOfWork.SaveChangesAsync(cancellationToken);
        }
    }
}
