using Catalog.API.Products.GetById;

namespace Catalog.API.Products.Get;

internal class GetProductsQueryHandler(IDocumentSession session, ILogger<GetProductsQueryHandler> logger) 
    : IQueryHandler<GetProductsQuery, GetProductsResult>
{
    public async Task<GetProductsResult> Handle(GetProductsQuery query, CancellationToken cancellationToken)
    {
        logger.LogInformation($"{nameof(GetProductsQueryHandler)}.Handle called with {query}");

        IReadOnlyList<Product> products = await session.Query<Product>().ToListAsync(cancellationToken);

        return new GetProductsResult(products);
    }
}
