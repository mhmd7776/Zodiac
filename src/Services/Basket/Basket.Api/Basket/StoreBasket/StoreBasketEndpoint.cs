using Basket.Api.Models;

namespace Basket.Api.Basket.StoreBasket
{
    #region Request

    public record StoreBasketRequest(ShoppingCart ShoppingCart);

    #endregion

    #region Response

    public record StoreBasketResponse(string UserName);

    #endregion

    #region Endpoint

    public class StoreBasketEndpoint : ICarterModule
    {
        public void AddRoutes(IEndpointRouteBuilder app)
        {
            app.MapPost("/Basket", async (StoreBasketRequest request, ISender sender) =>
            {
                // map request to command
                var command = request.Adapt<StoreBasketCommand>();

                // send command to handler and get result
                var result = await sender.Send(command);

                // map handler result to response
                var response = result.Adapt<StoreBasketResponse>();

                return Results.Ok(response);
            }).WithName("StoreBasket")
            .Produces<StoreBasketResponse>(StatusCodes.Status200OK)
            .ProducesProblem(StatusCodes.Status400BadRequest)
            .WithSummary("Store basket")
            .WithDescription("Store basket");
        }
    }

    #endregion
}
