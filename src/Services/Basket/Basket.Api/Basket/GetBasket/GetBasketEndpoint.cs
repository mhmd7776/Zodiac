using Basket.Api.Models;

namespace Basket.Api.Basket.GetBasket
{
    #region Request

    public record GetBasketRequest();

    #endregion

    #region Response

    public record GetBasketResponse(ShoppingCart ShoppingCart);

    #endregion

    #region Endpoint

    public class GetBasketEndpoint : ICarterModule
    {
        public void AddRoutes(IEndpointRouteBuilder app)
        {
            app.MapGet("/Basket/{userName}", async (string userName, ISender sender) =>
            {
                // map request to query
                var query = new GetBasketQuery(userName);

                // send query to handler and get result
                var result = await sender.Send(query);

                // map handler result to response
                var response = result.Adapt<GetBasketResponse>();

                return Results.Ok(response);
            }).WithName("GetBasket")
            .Produces<GetBasketResponse>(StatusCodes.Status200OK)
            .ProducesProblem(StatusCodes.Status400BadRequest)
            .WithSummary("Get basket")
            .WithDescription("Get basket");
        }
    }

    #endregion
}
