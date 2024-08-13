namespace Basket.API.Basket.Queries.GetBasket;

public class GetBasketQueryHandler(IBasketRepository repository) : IQueryHandler<GetBasketQuery, GetBasketResult>
{
    public async Task<GetBasketResult> Handle(GetBasketQuery query, CancellationToken cancellationToken)
    {
        ShoppingCart basket = await repository.GetBasket(query.UserName);

        return new GetBasketResult(basket);
    }
}
