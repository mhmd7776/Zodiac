using BuildingBlocks.Exceptions;

namespace Basket.Api.Exceptions
{
    public class BasketNotFoundException(string UserName) : NotFoundException("Basket", UserName)
    {
    }
}
