namespace Ordering.Domain.ValueObjects.TypeIds;
public record ProductId
{
    public Guid Value { get; }
    private ProductId(Guid value) => Value = value;

    public static ProductId Of(Guid value)
    {
        ArgumentNullException.ThrowIfNull(value);
        if (value == Guid.Empty)
        {
            throw new DomainException("ProductId cannot be empty.");
        }

        return new ProductId(value);
    }

    public static implicit operator Guid(ProductId name) => name.Value;

    public static implicit operator ProductId(Guid name) => new(name);
}
