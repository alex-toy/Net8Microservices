namespace Catalog.API.Products.Create;

internal class CreateProductCommandHandler(IDocumentSession session, ILogger<UpdateProductCommandHandler> logger) 
    : ICommandHandler<CreateProductCommand, CreateProductResult>
{
    public async Task<CreateProductResult> Handle(CreateProductCommand command, CancellationToken cancellationToken)
    {
        logger.LogInformation($"{nameof(CreateProductCommandHandler)} called with {command}");

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

        return new CreateProductResult(product.Id);
    }
}
