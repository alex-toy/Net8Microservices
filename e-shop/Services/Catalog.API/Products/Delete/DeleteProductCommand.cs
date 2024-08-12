namespace Catalog.API.Products.Delete;

public record DeleteProductCommand(Guid Id) : ICommand<DeleteProductResult>;