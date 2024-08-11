namespace Catalog.API.Products.Delete;

public class DeleteProductEndoint : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapGet("/products/{id}", async (Guid id, ISender sender) =>
        {
            try
            {
                DeleteProductResult result = await sender.Send(new DeleteProductCommand(id));
                DeleteProductResponse response = result.Adapt<DeleteProductResponse>();
                return Results.Ok(response);
            }
            catch (Exception)
            {
                throw;
            }
        })
        .WithName("DeleteProduct")
        .Produces<DeleteProductResponse>(StatusCodes.Status200OK)
        .ProducesProblem(StatusCodes.Status400BadRequest)
        .WithSummary("Delete Product")
        .WithDescription("Delete Product");
    }
}
