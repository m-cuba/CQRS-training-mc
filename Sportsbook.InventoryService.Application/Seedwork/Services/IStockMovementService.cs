namespace Sportsbook.InventoryService.Application.Seedwork.Services
{
    public interface IStockMovementService
    {
        Task<bool> ExistsAsync(Guid guid);
    }
}
