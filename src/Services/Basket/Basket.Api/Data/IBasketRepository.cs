using Basket.Api.Models;

namespace Basket.Api.Data
{
    internal interface IBasketRepository
    {
        Task<ShoppingCart> GetShoppingCartAsync(string userName, CancellationToken cancellationToken = default);
        Task<ShoppingCart> StoreShoppingCartAsync(ShoppingCart shoppingCart, CancellationToken cancellationToken = default);
        Task DeleteShoppingCartAsync(string userName, CancellationToken cancellationToken = default);
    }
}
