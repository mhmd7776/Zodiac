using Basket.Api.Data;
using Basket.Api.Models;
using BuildingBlocks.CQRS;
using Discount.Grpc.Protos;
using static Discount.Grpc.Protos.DiscountProtoService;

namespace Basket.Api.Basket.StoreBasket
{
    #region Command

    public record StoreBasketCommand(ShoppingCart ShoppingCart) : ICommand<StoreBasketResult>;

    #endregion

    #region Result

    public record StoreBasketResult(string UserName);

    #endregion

    #region Validator

    public class StoreBasketValidator : AbstractValidator<StoreBasketCommand>
    {
        public StoreBasketValidator()
        {
            RuleFor(x => x.ShoppingCart).NotNull()
                .WithMessage("ShoppingCart can not be null");

            RuleFor(x => x.ShoppingCart.UserName).NotEmpty()
                .WithMessage("UserName can not be empty");
        }
    };

    #endregion

    #region Handler

    internal class StoreBasketCommandHandler(IBasketRepository basketRepository, DiscountProtoService.DiscountProtoServiceClient discountProtoServiceClient) : ICommandHandler<StoreBasketCommand, StoreBasketResult>
    {
        public async Task<StoreBasketResult> Handle(StoreBasketCommand command, CancellationToken cancellationToken)
        {
            // Get discount from Discount.Grpc
            foreach (var item in command.ShoppingCart.Items)
            {
                // get coupon for item product
                var getDiscountRequest = new GetDiscountRequest { ProductId = item.ProductId.ToString() };
                var coupon = await discountProtoServiceClient.GetDiscountAsync(getDiscountRequest, cancellationToken: cancellationToken);

                item.Price = ApplyCoupon(coupon, item.Price);
            }

            var shoppingCart = await basketRepository.StoreShoppingCartAsync(command.ShoppingCart, cancellationToken);
            return new StoreBasketResult(shoppingCart.UserName!);
        }

        private static decimal ApplyCoupon(CouponModel coupon, decimal price)
        {
            if (coupon == null || coupon.Amount <= 0)
                return price;

            if (coupon.IsPercentage && coupon.Amount >= 100)
                return decimal.Zero;

            if (!coupon.IsPercentage && coupon.Amount >= price)
                return decimal.Zero;

            if (coupon.IsPercentage)
            {
                price -= price * (coupon.Amount / 100M);
            }
            else
            {
                price -= coupon.Amount;
            }

            return Math.Round(price);
        }
    }

    #endregion
}
