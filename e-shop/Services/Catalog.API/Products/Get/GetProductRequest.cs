namespace Catalog.API.Products.Get;

public record GetProductRequest(int? PageNumber = 1, int? PageSize = 10);
