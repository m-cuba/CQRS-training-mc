namespace Sportsbook.InventoryService.Core.InventoryContext
{
    public sealed record Quantity
    {
        public int Value { get; }

        public Quantity(int value)
        {
            if (value < 0)
                throw new ArgumentException("Quantity cannot be negative", nameof(value));

            Value = value;
        }

        public Quantity Add(Quantity other)
            => new(Value + other.Value);

        public Quantity Subtract(Quantity other)
        {
            if (Value - other.Value < 0)
                throw new InvalidOperationException("Quantity cannot be negative");

            return new(Value - other.Value);
        }

        public static Quantity operator +(Quantity left, Quantity right)
            => left.Add(right);

        public static Quantity operator -(Quantity left, Quantity right)
            => left.Subtract(right);

        public static bool operator <(Quantity left, Quantity right)
            => left.Value < right.Value;

        public static bool operator >(Quantity left, Quantity right)
            => left.Value > right.Value;

        public bool IsZero => Value == 0;

        public override string ToString() => Value.ToString();
    }
}
