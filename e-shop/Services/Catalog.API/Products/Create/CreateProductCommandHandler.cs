using Catalog.API.Products.GetById;
using Microsoft.Extensions.Logging;

namespace Catalog.API.Products.Create;

internal class GetProductCommandHandler(IDocumentSession session) : ICommandHandler<GetProductCommand, GetProductResult>
{
    public async Task<GetProductResult> Handle(GetProductCommand command, CancellationToken cancellationToken)
    {
        Product product = new()
        {
            Name = command.Name,
            Description = command.Description,
            Category = command.Category,
            ImageFile = command.ImageFile,
            Price = command.Price
        };

        session.Store(product);
        await session.SaveChangesAsync(cancellationToken);

        return new GetProductResult(product.Id);
    }
}
