using FluentValidation;
using Sportsbook.InventoryService.Application.Seedwork.Interfaces;
using Sportsbook.InventoryService.Core.Repositories;
using Sportsbook.InventoryService.Core.ValueObjects;

namespace Sportsbook.InventoryService.Application.Features.InventoryItemContext.RemoveStock
{
    public class RemoveStockCommandHandler(
        IInventoryRepository repository,
        IUnitOfWork unitOfWork,
        IValidator<RemoveStockCommand> validator) : IRemoveStockCommandHandler
    {
        public async Task Handle(RemoveStockCommand command, CancellationToken cancellationToken = default)
        {
            await validator.ValidateAndThrowAsync(command, cancellationToken);

            var item = await repository.GetBySkuAsync(new Sku(command.Sku), cancellationToken)
                ?? throw new InvalidOperationException("Item not found");

            item.RemoveStock(new Quantity(command.Quantity));

            await unitOfWork.SaveChangesAsync(cancellationToken);
        }
    }
}
