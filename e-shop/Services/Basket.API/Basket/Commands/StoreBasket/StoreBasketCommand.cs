using Basket.API.Basket.Queries.StoreBasket;

namespace Basket.API.Basket.Commands.StoreBasket;

public record StoreBasketCommand(ShoppingCart Cart) : ICommand<StoreBasketResult>;