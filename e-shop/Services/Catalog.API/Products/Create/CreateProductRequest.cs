namespace Catalog.API.Products.Create;

public record GetProductRequest(string Name, List<string> Category, string Description, string ImageFile, decimal Price);
    