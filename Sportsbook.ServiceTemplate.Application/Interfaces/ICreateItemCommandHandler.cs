using Sportsbook.ServiceTemplate.Application.Commands.CreateItem;

namespace Sportsbook.ServiceTemplate.Application.Interfaces
{
    public interface ICreateItemCommandHandler
    {
        Task Handle(CreateItemCommand command, CancellationToken cancellationToken = default);
    }
}
