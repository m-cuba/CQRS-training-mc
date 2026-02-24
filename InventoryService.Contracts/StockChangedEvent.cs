namespace InventoryService.Contracts
{
    public sealed record StockChangedEvent(
        string Sku,
        string Name,
        int Quantity,
        DateTimeOffset OccurredAt,
        Guid MovementId
    ) : IDomainEvent;
}
