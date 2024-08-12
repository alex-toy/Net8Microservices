using Catalog.API.Products.Create;
using MediatR;

namespace Catalog.API.Products.Get;

public class GetProductEndoint : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapGet("/products", async([AsParameters] GetProductRequest request, ISender sender) =>
        {
            GetProductsQuery query = request.Adapt<GetProductsQuery>();

            GetProductsResult result = await sender.Send(query);

            GetProductsResponse response = result.Adapt<GetProductsResponse>();

            return Results.Ok(response);
        })
        .WithName("GetProducts")
        .Produces<GetProductsResponse>(StatusCodes.Status200OK)
        .ProducesProblem(StatusCodes.Status400BadRequest)
        .WithSummary("Get Products")
        .WithDescription("Get Products");
    }
}
