namespace Catalog.API.Products.Delete;

public record DeleteProductCommand(Guid Id) : IQuery<DeleteProductResult>;