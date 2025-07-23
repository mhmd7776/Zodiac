using Basket.Api.Models;
using Microsoft.Extensions.Caching.Distributed;
using System.Text.Json;

namespace Basket.Api.Data
{
    internal class CachedBasketRepository(IBasketRepository basketRepository, IDistributedCache cache) : IBasketRepository
    {
        #region Methods

        public async Task<ShoppingCart> GetShoppingCartAsync(string userName, CancellationToken cancellationToken = default)
        {
            var cachedShoppingCart = await cache.GetStringAsync(userName, cancellationToken);
            if (!string.IsNullOrEmpty(cachedShoppingCart))
                return JsonSerializer.Deserialize<ShoppingCart>(cachedShoppingCart) ?? throw new JsonException("Can not deserialize json ShoppingCart");

            var shoppingCart = await basketRepository.GetShoppingCartAsync(userName, cancellationToken);
            await cache.SetStringAsync(userName, JsonSerializer.Serialize(shoppingCart), cancellationToken);

            return shoppingCart;
        }

        public async Task<ShoppingCart> StoreShoppingCartAsync(ShoppingCart shoppingCart, CancellationToken cancellationToken = default)
        {
            await basketRepository.StoreShoppingCartAsync(shoppingCart, cancellationToken);
            await cache.SetStringAsync(shoppingCart.UserName!, JsonSerializer.Serialize(shoppingCart), cancellationToken);

            return shoppingCart;
        }

        public async Task DeleteShoppingCartAsync(string userName, CancellationToken cancellationToken = default)
        {
            await basketRepository.DeleteShoppingCartAsync(userName, cancellationToken);
            await cache.RemoveAsync(userName, cancellationToken);
        }

        #endregion
    }
}
