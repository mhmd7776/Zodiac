using Basket.Api.Exceptions;
using Basket.Api.Models;

namespace Basket.Api.Data
{
    public class BasketRepository(IDocumentSession session) : IBasketRepository
    {
        #region Methods

        public async Task<ShoppingCart> GetShoppingCartAsync(string userName, CancellationToken cancellationToken = default)
        {
            var basket = await session.LoadAsync<ShoppingCart>(userName, cancellationToken);
            if (basket is null)
                throw new BasketNotFoundException(userName);

            return basket;
        }

        public async Task<ShoppingCart> StoreShoppingCartAsync(ShoppingCart shoppingCart, CancellationToken cancellationToken = default)
        {
            session.Store(shoppingCart);
            await session.SaveChangesAsync(cancellationToken);

            return shoppingCart;
        }

        public async Task DeleteShoppingCartAsync(string userName, CancellationToken cancellationToken = default)
        {
            session.Delete<ShoppingCart>(userName);
            await session.SaveChangesAsync(cancellationToken);
        }

        #endregion
    }
}
