namespace InventoryService.Audit.Core
{
    public class AuditStockEntry(Guid movementId, string sku, string name, int quantityDelta, DateTimeOffset occurredOn)
    {
        public Guid MovementId { get; } = movementId;
        public string Sku { get; } = sku;
        public int QuantityDelta { get; } = quantityDelta;

        public string Name { get; } = name;

        public DateTimeOffset OccurredOn { get; } = occurredOn;
    }
}
