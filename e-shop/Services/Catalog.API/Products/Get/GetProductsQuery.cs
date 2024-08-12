namespace Catalog.API.Products.Get;

public record GetProductsQuery(int PageNumber, int PageSize) : IQuery<GetProductsResult>;
