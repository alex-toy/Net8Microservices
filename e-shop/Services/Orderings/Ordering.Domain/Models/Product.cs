using Ordering.Domain.ValueObjects.Products;
using Ordering.Domain.ValueObjects.TypeIds;

namespace Ordering.Domain.Models;

public class Product : Entity<ProductId>
{
    public string Name { get; private set; } = default!;
    public decimal Price { get; private set; } = default!;

    public static Product Create(ProductId id, ProductName name, ProductPrice price)
    {
        var product = new Product
        {
            Id = id,
            Name = name,
            Price = price
        };

        return product;
    }
}
