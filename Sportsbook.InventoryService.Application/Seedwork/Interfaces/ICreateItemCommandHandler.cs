using Sportsbook.InventoryService.Application.Features.InventoryItemContext.CreateItem;

namespace Sportsbook.InventoryService.Application.Seedwork.Interfaces
{
    public interface ICreateItemCommandHandler
    {
        Task Handle(CreateItemCommand command, CancellationToken cancellationToken = default);
    }
}
