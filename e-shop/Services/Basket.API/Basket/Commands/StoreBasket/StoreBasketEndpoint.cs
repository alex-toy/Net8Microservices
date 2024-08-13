namespace Basket.API.Basket.Commands.StoreBasket;

public class StoreBasketEndpoint : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapPost("/basket/", async (StoreBasketRequest request, ISender sender) =>
        {
            StoreBasketCommand command = request.Adapt<StoreBasketCommand>();

            StoreBasketResult result = await sender.Send(command);

            StoreBasketResponse response = result.Adapt<StoreBasketResponse>();

            return Results.Created($"/basket/{response.UserName}", response);
        })
        .WithName("StoreBasket")
        .Produces<StoreBasketResponse>(StatusCodes.Status200OK)
        .ProducesProblem(StatusCodes.Status400BadRequest)
        .WithSummary("Store Basket")
        .WithDescription("Store Basket");
    }
}
