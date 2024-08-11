using Catalog.API.Exceptions;
using Catalog.API.Products.Update;

namespace Catalog.API.Products.Create;

internal class UpdateProductCommandHandler(IDocumentSession session, ILogger<UpdateProductCommandHandler> logger) 
    : ICommandHandler<UpdateProductCommand, UpdateProductResult>
{
    public async Task<UpdateProductResult> Handle(UpdateProductCommand command, CancellationToken cancellationToken)
    {
        logger.LogInformation($"{nameof(UpdateProductCommandHandler)} called with {command}");

        Product? product = await session.LoadAsync<Product>(command.Id, cancellationToken);

        if (product is null) throw new ProductNotFoundException(command.Id);

        Update(product, command);

        session.Update(product);
        await session.SaveChangesAsync(cancellationToken);

        return new UpdateProductResult(true);
    }

    private void Update(Product product, UpdateProductCommand command)
    {
        product.Name = command.Name;
        product.Description = command.Description;
        product.Category = command.Category;
        product.ImageFile = command.ImageFile;
        product.Price = command.Price;
    }
}
