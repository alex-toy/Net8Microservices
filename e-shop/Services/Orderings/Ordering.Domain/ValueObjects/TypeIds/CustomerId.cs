namespace Ordering.Domain.ValueObjects.TypeIds;

public record CustomerId : TypeId
{
    //public Guid Value { get; }
    //private CustomerId(Guid value) => Value = value;

    public CustomerId(Guid id) : base(id)
    {
        ArgumentNullException.ThrowIfNull(id);
        if (id == Guid.Empty) throw new DomainException($"{nameof(CustomerId)} cannot be empty.");
    }

    //public static CustomerId Of(Guid value)
    //{
    //    ArgumentNullException.ThrowIfNull(value);
    //    if (value == Guid.Empty) throw new DomainException($"{nameof(CustomerId)} cannot be empty.");

    //    return new CustomerId(value);
    //}

    public static implicit operator Guid(CustomerId name) => name.Value;

    public static implicit operator CustomerId(Guid name) => new(name);
}
