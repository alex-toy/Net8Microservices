namespace Ordering.Domain.ValueObjects.Customers;

public record Email
{
    public string Value { get; }
    private Email(string value) => Value = value;

    public static Email Of(string value)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(value);

        return new Email(value);
    }

    public static implicit operator string(Email name) => name.Value;

    public static implicit operator Email(string name) => new(name);
}
