namespace Sportsbook.InventoryService.Application.Seedwork.Interfaces
{
    public interface IReadUnitOfWork
    {
        Task SaveChangesAsync(CancellationToken cancellationToken = default);
    }
}
