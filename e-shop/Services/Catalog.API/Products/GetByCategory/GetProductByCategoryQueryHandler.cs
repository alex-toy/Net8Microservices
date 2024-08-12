using Catalog.API.Exceptions;

namespace Catalog.API.Products.GetByCategory;

internal class GetProductByCategoryQueryHandler(IDocumentSession session) : IQueryHandler<GetProductByCategoryQuery, GetProductByCategoryResult>
{
    public async Task<GetProductByCategoryResult> Handle(GetProductByCategoryQuery query, CancellationToken cancellationToken)
    {
        IReadOnlyList<Product>? products = await session.Query<Product>().Where(p => p.Category.Contains(query.Category)).ToListAsync();

        if (products is null ) throw new ProductNotFoundException(query.Category);

        return new GetProductByCategoryResult(products);
    }
}
