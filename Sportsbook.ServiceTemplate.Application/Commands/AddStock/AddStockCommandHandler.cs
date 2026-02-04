using Sportsbook.ServiceTemplate.Application.Interfaces;
using Sportsbook.ServiceTemplate.Core.ValueObjects;

namespace Sportsbook.ServiceTemplate.Application.Commands.AddStock
{
    public class AddStockCommandHandler(IInventoryRepository repository) : IAddStockCommandHandler
    {
        public async Task Handle(AddStockCommand command, CancellationToken cancellationToken = default)
        {
            var item = await repository.GetBySkuAsync(new Sku(command.Sku))
                ?? throw new InvalidOperationException("Item not found");

            item.AddStock(command.Quantity);

            await repository.UpdateAsync(item);
        }
    }
}
