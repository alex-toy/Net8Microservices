using Catalog.API.Exceptions;

namespace Catalog.API.Products.GetById;

internal class GetProductByIdQueryHandler(IDocumentSession session, ILogger<GetProductByIdQueryHandler> logger) 
    : IQueryHandler<GetProductByCategoryQuery, GetProductByIdResult>
{
    public async Task<GetProductByIdResult> Handle(GetProductByCategoryQuery query, CancellationToken cancellationToken)
    {
        logger.LogInformation($"{nameof(GetProductByIdQueryHandler)}.Handle called with {query}");

        Product? products = await session.LoadAsync<Product>(query.Id, cancellationToken);

        if (products is null ) throw new ProductNotFoundException(query.Id);

        return new GetProductByIdResult(products);
    }
}
