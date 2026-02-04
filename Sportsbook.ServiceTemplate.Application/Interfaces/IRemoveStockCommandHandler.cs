using Sportsbook.ServiceTemplate.Application.Commands.RemoveStock;

namespace Sportsbook.ServiceTemplate.Application.Interfaces
{
    public interface IRemoveStockCommandHandler
    {
        Task Handle(RemoveStockCommand command, CancellationToken cancellationToken = default);
    }
}
