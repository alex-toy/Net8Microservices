using Discount.Grpc;

namespace Basket.API.Basket.Commands.StoreBasket;

public class StoreBasketCommandHandler(IBasketRepository repository, DiscountProtoService.DiscountProtoServiceClient discountProto) 
    : ICommandHandler<StoreBasketCommand, StoreBasketResult>
{
    public async Task<StoreBasketResult> Handle(StoreBasketCommand command, CancellationToken cancellationToken)
    {
        await DeductDiscount(command.Cart, cancellationToken);

        await repository.StoreBasket(command.Cart, cancellationToken);

        return new StoreBasketResult(command.Cart.UserName);
    }

    private async Task DeductDiscount(ShoppingCart cart, CancellationToken cancellationToken)
    {
        foreach (var item in cart.Items)
        {
            GetDiscountRequest request = new GetDiscountRequest { ProductName = item.ProductName };
            CouponModel coupon = await discountProto.GetDiscountAsync(request, cancellationToken: cancellationToken);
            item.Price -= coupon.Amount;
        }
    }
}
