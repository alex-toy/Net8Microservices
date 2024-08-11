namespace Catalog.API.Products.Create;

public record GetProductCommand(string Name, List<string> Category, string Description, string ImageFile, decimal Price) : ICommand<GetProductResult>;
