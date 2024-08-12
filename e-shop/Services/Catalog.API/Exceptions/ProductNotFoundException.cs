using BuildingBlocs.Exceptions;

namespace Catalog.API.Exceptions;

public class ProductNotFoundException : NotFoundException
{
    public ProductNotFoundException(Guid productId) : base("Product", productId)
    {
    }

    public ProductNotFoundException(string category) : base($"Product with category {category} not found")
    {
    }
}
