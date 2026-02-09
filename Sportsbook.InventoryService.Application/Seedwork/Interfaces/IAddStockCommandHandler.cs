using Sportsbook.InventoryService.Application.Features.InventoryItemContext.AddStock;

namespace Sportsbook.InventoryService.Application.Seedwork.Interfaces
{
    public interface IAddStockCommandHandler
    {
        Task Handle(AddStockCommand command, CancellationToken cancellationToken = default);
    }
}
