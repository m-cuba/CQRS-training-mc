using Sportsbook.InventoryService.Application.Features.InventoryItemContext.RemoveStock;

namespace Sportsbook.InventoryService.Application.Seedwork.Interfaces
{
    public interface IRemoveStockCommandHandler
    {
        Task Handle(RemoveStockCommand command, CancellationToken cancellationToken = default);
    }
}
