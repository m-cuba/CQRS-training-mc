namespace Sportsbook.InventoryService.Application.Features.InventoryItemContext.RemoveStock
{
    public record RemoveStockCommand(string Sku, int Quantity);

}
