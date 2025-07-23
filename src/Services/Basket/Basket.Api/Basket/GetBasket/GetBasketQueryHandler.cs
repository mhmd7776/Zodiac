using Basket.Api.Data;
using Basket.Api.Models;
using BuildingBlocks.CQRS;

namespace Basket.Api.Basket.GetBasket
{
    #region Query

    public record GetBasketQuery(string UserName) : IQuery<GetBasketResult>;

    #endregion

    #region Result

    public record GetBasketResult(ShoppingCart ShoppingCart);

    #endregion

    #region Handler

    internal class GetBasketQueryHandler(IBasketRepository basketRepository) : IQueryHandler<GetBasketQuery, GetBasketResult>
    {
        public async Task<GetBasketResult> Handle(GetBasketQuery query, CancellationToken cancellationToken)
        {
            var shoppingCart = await basketRepository.GetShoppingCartAsync(query.UserName, cancellationToken);
            return new GetBasketResult(shoppingCart);
        }
    }

    #endregion
}
