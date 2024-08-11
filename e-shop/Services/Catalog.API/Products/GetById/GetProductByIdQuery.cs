namespace Catalog.API.Products.GetById;

public record GetProductByCategoryQuery(Guid Id) : IQuery<GetProductByIdResult>;
