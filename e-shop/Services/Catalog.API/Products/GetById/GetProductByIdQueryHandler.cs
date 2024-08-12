using Catalog.API.Exceptions;

namespace Catalog.API.Products.GetById;

internal class GetProductByIdQueryHandler(IDocumentSession session) : IQueryHandler<GetProductByCategoryQuery, GetProductByIdResult>
{
    public async Task<GetProductByIdResult> Handle(GetProductByCategoryQuery query, CancellationToken cancellationToken)
    {
        Product? products = await session.LoadAsync<Product>(query.Id, cancellationToken);

        if (products is null ) throw new ProductNotFoundException(query.Id);

        return new GetProductByIdResult(products);
    }
}
