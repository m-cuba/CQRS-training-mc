using FluentValidation;
using Sportsbook.InventoryService.Application.Seedwork.Interfaces;
using Sportsbook.InventoryService.Core.Repositories;
using Sportsbook.InventoryService.Core.ValueObjects;

namespace Sportsbook.InventoryService.Application.Features.InventoryItemContext.AddStock
{
    public class AddStockCommandHandler(
        IInventoryRepository repository,
        IUnitOfWork unitOfWork,
        IValidator<AddStockCommand> validator) : IAddStockCommandHandler
    {
        public async Task Handle(AddStockCommand command, CancellationToken cancellationToken = default)
        {
            await validator.ValidateAndThrowAsync(command, cancellationToken);

            var item = await repository.GetBySkuAsync(new Sku(command.Sku), cancellationToken)
                ?? throw new InvalidOperationException("Item not found");

            item.AddStock(new Quantity(command.Quantity));

            await unitOfWork.SaveChangesAsync(cancellationToken);
        }
    }
}
