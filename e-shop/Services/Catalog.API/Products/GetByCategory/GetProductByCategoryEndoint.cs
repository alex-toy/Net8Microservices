namespace Catalog.API.Products.GetByCategory;

public class GetProductByCategoryEndoint : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapGet("/products/{category}", async (string category, ISender sender) =>
        {
            try
            {
                GetProductByCategoryResult result = await sender.Send(new GetProductByCategoryQuery(category));
                GetProductByCategoryResponse response = result.Adapt<GetProductByCategoryResponse>();
                return Results.Ok(response);
            }
            catch (Exception)
            {
                throw;
            }
        })
        .WithName("GetProductByCategory")
        .Produces<GetProductByCategoryResponse>(StatusCodes.Status200OK)
        .ProducesProblem(StatusCodes.Status400BadRequest)
        .WithSummary("Get Products By Category")
        .WithDescription("Get Products By Category");
    }
}
