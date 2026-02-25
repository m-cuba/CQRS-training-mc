namespace InventoryService.Contracts
{
    public interface IDomainEvent
    {
        Guid MovementId { get; }
        DateTimeOffset OccurredAt { get; }

        string Sku { get; }

        string Name { get; }

        int Quantity { get; }
    }
}
