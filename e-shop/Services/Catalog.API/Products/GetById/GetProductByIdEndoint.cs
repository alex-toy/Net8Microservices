namespace Catalog.API.Products.GetById;

public class GetProductByIdEndoint : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapGet("/products/{id}", async (Guid id, ISender sender) =>
        {
            try
            {
                GetProductByIdResult result = await sender.Send(new GetProductByCategoryQuery(id));
                GetProductByIdResponse response = result.Adapt<GetProductByIdResponse>();
                return Results.Ok(response);
            }
            catch (Exception)
            {
                throw;
            }
        })
        .WithName("GetProductById")
        .Produces<GetProductByIdResponse>(StatusCodes.Status200OK)
        .ProducesProblem(StatusCodes.Status400BadRequest)
        .WithSummary("Get Products By Id")
        .WithDescription("Get Products By Id");
    }
}
