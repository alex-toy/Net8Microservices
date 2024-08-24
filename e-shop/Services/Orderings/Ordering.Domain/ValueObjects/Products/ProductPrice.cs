namespace Ordering.Domain.ValueObjects.Products;

public record ProductPrice
{
    public decimal Value { get; }

    public ProductPrice(decimal price)
    {
        ArgumentOutOfRangeException.ThrowIfNegativeOrZero(price);

        Value = price;
    }

    public static implicit operator decimal(ProductPrice name) => name.Value;

    public static implicit operator ProductPrice(decimal name) => new(name);
}
