namespace Basket.API.Basket.Queries.GetBasket;

public record GetBasketQuery(string UserName) : IQuery<GetBasketResult>;
