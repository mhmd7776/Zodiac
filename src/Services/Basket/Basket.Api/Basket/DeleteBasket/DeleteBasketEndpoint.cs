using Basket.Api.Basket.StoreBasket;

namespace Basket.Api.Basket.DeleteBasket
{
    #region Request

    public record DeleteBasketRequest();

    #endregion

    #region Response

    public record DeleteBasketResponse();

    #endregion

    #region Endpoint

    public class DeleteBasketEndpoint : ICarterModule
    {
        public void AddRoutes(IEndpointRouteBuilder app)
        {
            app.MapDelete("/Basket/{userName}", async (string userName, ISender sender) =>
            {
                // map request to command
                var command = new DeleteBasketCommand(userName);

                // send command to handler and get result
                var result = await sender.Send(command);

                return Results.Ok();
            }).WithName("DeleteBasket")
            .Produces(StatusCodes.Status200OK)
            .ProducesProblem(StatusCodes.Status400BadRequest)
            .WithSummary("Delete basket")
            .WithDescription("Delete basket");
        }
    }

    #endregion
}
