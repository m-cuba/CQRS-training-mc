namespace Sportsbook.InventoryService.Core.InventoryContext.Events
{
    public sealed record StockChangedEvent(
        string Sku,
        string Name,
        int Quantity,
        DateTimeOffset OccurredAt,
        Guid MovementId
    );
}
