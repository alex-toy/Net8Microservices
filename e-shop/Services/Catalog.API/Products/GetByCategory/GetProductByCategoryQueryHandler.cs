using Catalog.API.Exceptions;

namespace Catalog.API.Products.GetByCategory;

internal class GetProductByCategoryQueryHandler(IDocumentSession session, ILogger<GetProductByCategoryQueryHandler> logger) 
    : IQueryHandler<GetProductByCategoryQuery, GetProductByCategoryResult>
{
    public async Task<GetProductByCategoryResult> Handle(GetProductByCategoryQuery query, CancellationToken cancellationToken)
    {
        logger.LogInformation($"{nameof(GetProductByCategoryQueryHandler)}.Handle called with {query}");

        Product? products = await session.LoadAsync<Product>(query.Category, cancellationToken);

        if (products is null ) throw new ProductNotFoundException(query.Category);

        return new GetProductByCategoryResult(products);
    }
}
