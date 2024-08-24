namespace Ordering.Domain.ValueObjects.TypeIds;

public record OrderId : TypeId
{
    //public Guid Value { get; }
    //private OrderId(Guid value) => Value = value;

    public OrderId(Guid id) : base(id)
    {
        ArgumentNullException.ThrowIfNull(id);
        if (id == Guid.Empty) throw new DomainException($"{nameof(OrderId)} cannot be empty.");
    }

    //public static OrderId Of(Guid id)
    //{
    //    ArgumentNullException.ThrowIfNull(id);
    //    if (id == Guid.Empty) throw new DomainException($"{nameof(OrderId)} cannot be empty.");

    //    return new OrderId(id);
    //}

    public static implicit operator Guid(OrderId name) => name.Value;

    public static implicit operator OrderId(Guid name) => new(name);
}
