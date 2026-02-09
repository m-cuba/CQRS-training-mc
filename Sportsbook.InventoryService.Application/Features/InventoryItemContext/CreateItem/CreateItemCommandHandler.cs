using FluentValidation;
using Sportsbook.InventoryService.Application.Seedwork.Interfaces;
using Sportsbook.InventoryService.Core.Entities;
using Sportsbook.InventoryService.Core.Repositories;
using Sportsbook.InventoryService.Core.ValueObjects;

namespace Sportsbook.InventoryService.Application.Features.InventoryItemContext.CreateItem
{
    public class CreateItemCommandHandler(
        IInventoryRepository repository,
        IUnitOfWork unitOfWork,
        IValidator<CreateItemCommand> validator) : ICreateItemCommandHandler
    {
        public async Task Handle(CreateItemCommand command, CancellationToken cancellationToken = default)
        {
            await validator.ValidateAndThrowAsync(command, cancellationToken);

            var item = new InventoryItem(
                new Sku(command.Sku),
                command.Name
            );

            await repository.AddAsync(item, cancellationToken);

            await unitOfWork.SaveChangesAsync(cancellationToken);
        }
    }
}
