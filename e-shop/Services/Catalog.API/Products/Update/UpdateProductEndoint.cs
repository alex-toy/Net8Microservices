namespace Catalog.API.Products.Update;

public class UpdateProductEndoint : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapPut("/products", async (UpdateProductRequest request, ISender sender) =>
        {
            UpdateProductCommand command = request.Adapt<UpdateProductCommand>();
            UpdateProductResult result = await sender.Send(command);
            UpdateProductResponse response = result.Adapt<UpdateProductResponse>();
            return Results.Created($"/products/{response.IsSuccess}", response);
        })
        .WithName("UpdateProduct")
        .Produces<UpdateProductResponse>(StatusCodes.Status201Created)
        .ProducesProblem(StatusCodes.Status400BadRequest)
        .WithSummary("Update Product")
        .WithDescription("Update Product");
    }
}
