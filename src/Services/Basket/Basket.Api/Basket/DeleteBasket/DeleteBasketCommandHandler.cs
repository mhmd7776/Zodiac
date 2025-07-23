using Basket.Api.Data;
using BuildingBlocks.CQRS;

namespace Basket.Api.Basket.DeleteBasket
{
    #region Command

    public record DeleteBasketCommand(string UserName) : ICommand;

    #endregion

    #region Result

    public record DeleteBasketResult();

    #endregion

    #region Validator

    public class DeleteBasketValidator : AbstractValidator<DeleteBasketCommand>
    {
        public DeleteBasketValidator()
        {
            RuleFor(x => x.UserName).NotEmpty()
                .WithMessage("UserName can not be empty");
        }
    };

    #endregion

    #region Handler

    internal class DeleteBasketCommandHandler(IBasketRepository basketRepository) : ICommandHandler<DeleteBasketCommand>
    {
        public async Task<Unit> Handle(DeleteBasketCommand command, CancellationToken cancellationToken)
        {
            await basketRepository.DeleteShoppingCartAsync(command.UserName, cancellationToken);
            return Unit.Value;
        }
    }

    #endregion
}
