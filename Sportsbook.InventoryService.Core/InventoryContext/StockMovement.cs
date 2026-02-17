using System;

namespace Sportsbook.InventoryService.Core.InventoryContext
{
    public class StockMovement
    {
        public Guid Id { get; set; }
        public string Sku { get; set; } = null!;
        public int QuantityChange { get; set; }
        public DateTimeOffset OccurredAt { get; set; }
    }
}