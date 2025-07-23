using Basket.Api.Data;
using Basket.Api.Models;
using BuildingBlocks.CQRS;

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

    internal class StoreBasketCommandHandler(IBasketRepository basketRepository) : ICommandHandler<StoreBasketCommand, StoreBasketResult>
    {
        public async Task<StoreBasketResult> Handle(StoreBasketCommand command, CancellationToken cancellationToken)
        {
            var shoppingCart = await basketRepository.StoreShoppingCartAsync(command.ShoppingCart, cancellationToken);
            return new StoreBasketResult(shoppingCart.UserName!);
        }
    }

    #endregion
}
