namespace Catalog.API.Exceptions;

public class ProductNotFoundException : Exception
{
    public ProductNotFoundException(Guid productId) : base($"Product with guid {productId} not found")
    {
    }

    public ProductNotFoundException(string category) : base($"Product with category {category} not found")
    {
    }
}
