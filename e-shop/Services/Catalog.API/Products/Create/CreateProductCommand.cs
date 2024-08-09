using MediatR;

namespace Catalog.API.Products.Create;

public record CreateProductCommand(string Name, List<string> Category, string Description, string ImageFile, decimal Price) : IRequest<CreateProductResult>;
