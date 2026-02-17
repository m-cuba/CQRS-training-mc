using FluentValidation;
using Sportsbook.InventoryService.Application.Seedwork.Interfaces;
using Sportsbook.InventoryService.Core.InventoryContext;
using Sportsbook.InventoryService.Core.InventoryContext.Events;
using Sportsbook.InventoryService.Core.InventoryContext.Repositories;

namespace Sportsbook.InventoryService.Application.Features.InventoryItemContext.RemoveStock
{
    public class RemoveStockCommandHandler(
        IInventoryRepository repository,
        IUnitOfWork unitOfWork,
        IValidator<RemoveStockCommand> validator,
        IEventPublisher eventPublisher) : IRemoveStockCommandHandler
    {
        public async Task Handle(RemoveStockCommand command, CancellationToken cancellationToken = default)
        {
            await validator.ValidateAndThrowAsync(command, cancellationToken);

            var item = await repository.GetBySkuAsync(new Sku(command.Sku), cancellationToken)
                ?? throw new InvalidOperationException("Item not found");

            item.RemoveStock(new Quantity(command.Quantity));

            await unitOfWork.SaveChangesAsync(cancellationToken);

            var stockRemovedEvent = new StockChangedEvent(
                item.Sku.Value,
                item.Name,
                -command.Quantity,
                DateTimeOffset.UtcNow,
                Guid.NewGuid());

            await eventPublisher.PublishAsync(stockRemovedEvent, cancellationToken);
        }
    }
}
