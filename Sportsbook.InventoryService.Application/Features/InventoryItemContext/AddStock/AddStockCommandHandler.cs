using FluentValidation;
using Sportsbook.InventoryService.Application.Seedwork.Interfaces;
using Sportsbook.InventoryService.Core.InventoryContext;
using Sportsbook.InventoryService.Core.InventoryContext.Events;
using Sportsbook.InventoryService.Core.InventoryContext.Repositories;

namespace Sportsbook.InventoryService.Application.Features.InventoryItemContext.AddStock
{
    public class AddStockCommandHandler(
        IInventoryRepository repository,
        IUnitOfWork unitOfWork,
        IValidator<AddStockCommand> validator,
        IEventPublisher eventPublisher) : IAddStockCommandHandler
    {
        public async Task Handle(AddStockCommand command, CancellationToken cancellationToken = default)
        {
            await validator.ValidateAndThrowAsync(command, cancellationToken);

            var item = await repository.GetBySkuAsync(new Sku(command.Sku), cancellationToken)
                ?? throw new InvalidOperationException("Item not found");

            item.AddStock(new Quantity(command.Quantity));

            await unitOfWork.SaveChangesAsync(cancellationToken);

            var stockAddedEvent = new StockChangedEvent(
                item.Sku.Value,
                item.Name,
                command.Quantity,
                DateTimeOffset.UtcNow,
                Guid.NewGuid());

            await eventPublisher.PublishAsync(stockAddedEvent, cancellationToken);
        }
    }
}
