namespace Catalog.API.Products.Create;

public class GetProductEndoint : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapPost("/products", async (GetProductRequest request, ISender sender) =>
        {
            GetProductCommand command = request.Adapt<GetProductCommand>();
            GetProductResult result = await sender.Send(command);
            CreateProductResponse response = result.Adapt<CreateProductResponse>();
            return Results.Created($"/products/{response.Id}", response);
        })
        .WithName("CreateProduct")
        .Produces<CreateProductResponse>(StatusCodes.Status201Created)
        .ProducesProblem(StatusCodes.Status400BadRequest)
        .WithSummary("Create Product")
        .WithDescription("Create Product");
    }
}
