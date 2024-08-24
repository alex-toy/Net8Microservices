using Ordering.Domain.ValueObjects.Customers;
using Ordering.Domain.ValueObjects.TypeIds;

namespace Ordering.Domain.Models;

public class Customer : Entity<CustomerId>
{
    public string Name { get; private set; } = default!;
    public string Email { get; private set; } = default!;

    public static Customer Create(CustomerId id, string name, Email email)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(name);

        var customer = new Customer
        {
            Id = id,
            Name = name,
            Email = email
        };

        return customer;
    }
}
