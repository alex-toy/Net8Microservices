namespace Ordering.Domain.ValueObjects;
public record OrderName
{
    public string Value { get; }
    private OrderName(string value) => Value = value;
    private const int DefaultLength = 5;

    public static OrderName Of(string value)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(value);
        ArgumentOutOfRangeException.ThrowIfNotEqual(value.Length, DefaultLength);

        return new OrderName(value);
    }

    public static implicit operator string(OrderName name) => name.Value;

    public static implicit operator OrderName(string name) => new(name);
}
