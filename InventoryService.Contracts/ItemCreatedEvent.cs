namespace InventoryService.Contracts
{
    public sealed record ItemCreatedEvent(
        string Sku,
        string Name,
        DateTimeOffset OccurredAt,
        Guid MovementId
    ) : IDomainEvent
    {
        public int Quantity => 0;
    }
}
