namespace Ordering.Domain.ValueObjects.TypeIds;
public record OrderItemId
{
    public Guid Value { get; }
    private OrderItemId(Guid value) => Value = value;

    public static OrderItemId Of(Guid value)
    {
        ArgumentNullException.ThrowIfNull(value);
        if (value == Guid.Empty)
        {
            throw new DomainException("OrderItemId cannot be empty.");
        }

        return new OrderItemId(value);
    }

    public static implicit operator Guid(OrderItemId name) => name.Value;

    public static implicit operator OrderItemId(Guid name) => new(name);
}
