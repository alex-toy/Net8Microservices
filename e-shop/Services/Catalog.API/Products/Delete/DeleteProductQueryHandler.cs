using Catalog.API.Exceptions;

namespace Catalog.API.Products.Delete;

internal class DeleteProductQueryHandler(IDocumentSession session, ILogger<DeleteProductQueryHandler> logger) 
    : IQueryHandler<DeleteProductCommand, DeleteProductResult>
{
    public async Task<DeleteProductResult> Handle(DeleteProductCommand command, CancellationToken cancellationToken)
    {
        logger.LogInformation($"{nameof(DeleteProductQueryHandler)} called with {command}");

        Product? product = await session.LoadAsync<Product>(command.Id, cancellationToken);

        if (product is null ) throw new ProductNotFoundException(command.Id);

        session.Delete(product);
        await session.SaveChangesAsync(cancellationToken);

        return new DeleteProductResult(true);
    }
}
