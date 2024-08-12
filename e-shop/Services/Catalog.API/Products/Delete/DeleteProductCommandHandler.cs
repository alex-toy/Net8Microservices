using Catalog.API.Exceptions;

namespace Catalog.API.Products.Delete;

internal class DeleteProductCommandHandler(IDocumentSession session, ILogger<DeleteProductCommandHandler> logger) 
    : ICommandHandler<DeleteProductCommand, DeleteProductResult>
{
    public async Task<DeleteProductResult> Handle(DeleteProductCommand command, CancellationToken cancellationToken)
    {
        logger.LogInformation($"{nameof(DeleteProductCommandHandler)} called with {command}");

        Product? product = await session.LoadAsync<Product>(command.Id, cancellationToken);

        if (product is null) throw new ProductNotFoundException(command.Id);

        session.Delete<Product>(command.Id);
        await session.SaveChangesAsync(cancellationToken);

        return new DeleteProductResult(true);
    }
}
