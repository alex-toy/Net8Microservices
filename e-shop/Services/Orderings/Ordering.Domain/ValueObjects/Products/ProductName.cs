namespace Ordering.Domain.ValueObjects.Products;

public record ProductName
{
    public string Value { get; }

    private const int MinLength = 3;

    public ProductName(string name)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(name);
        ArgumentOutOfRangeException.ThrowIfGreaterThan(name.Length, MinLength);

        Value = name;
    }

    public static implicit operator string(ProductName name) => name.Value;

    public static implicit operator ProductName(string name) => new(name);
}
