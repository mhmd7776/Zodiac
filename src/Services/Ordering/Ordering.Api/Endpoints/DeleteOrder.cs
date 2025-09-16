using Carter;
using MediatR;
using Ordering.Application.Orders.Commands.DeleteOrder;

namespace Ordering.Api.Endpoints
{
    #region Request

    public record DeleteOrderRequest();

    #endregion

    #region Response

    public record DeleteOrderResponse();

    #endregion

    #region Endpoint

    public class DeleteOrder : ICarterModule
    {
        public void AddRoutes(IEndpointRouteBuilder app)
        {
            app.MapDelete("/Orders/{id}", async (Guid id, ISender sender) =>
            {
                // map request to command
                var command = new DeleteOrderCommand(id);

                // send command to handler and get result
                var result = await sender.Send(command);

                return Results.Ok();
            }).WithName("DeleteOrder")
            .Produces(StatusCodes.Status200OK)
            .ProducesProblem(StatusCodes.Status400BadRequest)
            .WithSummary("Delete an order")
            .WithDescription("Delete an order");
        }
    }

    #endregion
}
