using Sportsbook.ServiceTemplate.Application.Commands.AddStock;

namespace Sportsbook.ServiceTemplate.Application.Interfaces
{
    public interface IAddStockCommandHandler
    {
        Task Handle(AddStockCommand command, CancellationToken cancellationToken = default);
    }
}
