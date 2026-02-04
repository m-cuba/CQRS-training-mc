using Sportsbook.ServiceTemplate.Core.ValueObjects;

namespace Sportsbook.ServiceTemplate.Core.Exceptions
{
    public class InsufficientStockException : Exception
    {
        public Sku Sku { get; }

        public InsufficientStockException(Sku sku)
            : base($"Insufficient stock for item with SKU '{sku.Value}'.")
        {
            Sku = sku;
        }
    }
}
