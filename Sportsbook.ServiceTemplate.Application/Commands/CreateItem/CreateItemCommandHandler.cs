using Sportsbook.ServiceTemplate.Application.Interfaces;
using Sportsbook.ServiceTemplate.Core.Entities;
using Sportsbook.ServiceTemplate.Core.ValueObjects;

namespace Sportsbook.ServiceTemplate.Application.Commands.CreateItem
{
    public class CreateItemCommandHandler(IInventoryRepository repository) : ICreateItemCommandHandler
    {
        public async Task Handle(CreateItemCommand command, CancellationToken cancellationToken = default)
        {
            var item = new InventoryItem(
                new Sku(command.Sku),
                command.Name
            );

            await repository.AddAsync(item);
        }

    }
}
