using Sportsbook.ServiceTemplate.Application.Interfaces;
using Sportsbook.ServiceTemplate.Core.ValueObjects;

namespace Sportsbook.ServiceTemplate.Application.Commands.RemoveStock
{
    public class RemoveStockCommandHandler(IInventoryRepository repository) : IRemoveStockCommandHandler
    {
        public async Task Handle(RemoveStockCommand command, CancellationToken cancellationToken = default)
        {
            var item = await repository.GetBySkuAsync(new Sku(command.Sku))
                ?? throw new InvalidOperationException("Item not found");

            item.RemoveStock(command.Quantity);

            await repository.UpdateAsync(item);
        }
    }
}
