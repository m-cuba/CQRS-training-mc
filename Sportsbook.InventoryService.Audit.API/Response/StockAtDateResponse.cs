namespace Sportsbook.InventoryService.Audit.API.Response
{
    public sealed record StockAtDateResponse(
        string Sku,
        DateTimeOffset Date,
        int Stock);
}
