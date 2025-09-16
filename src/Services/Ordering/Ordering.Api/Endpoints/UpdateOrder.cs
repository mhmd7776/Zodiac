using Carter;
using Mapster;
using MediatR;
using Ordering.Application.DTOs;
using Ordering.Application.Orders.Commands.UpdateOrder;

namespace Ordering.Api.Endpoints
{
    #region Request

    public record UpdateOrderRequest(OrderDto OrderDto);

    #endregion

    #region Response

    public record UpdateOrderResponse();

    #endregion

    #region Endpoint

    public class UpdateOrder : ICarterModule
    {
        public void AddRoutes(IEndpointRouteBuilder app)
        {
            app.MapPut("/Orders", async (UpdateOrderRequest request, ISender sender) =>
            {
                // map request to command
                var command = request.Adapt<UpdateOrderCommand>();

                // send command to handler and get result
                var result = await sender.Send(command);

                return Results.NoContent();
            }).WithName("UpdateOrder")
            .Produces(StatusCodes.Status204NoContent)
            .ProducesProblem(StatusCodes.Status400BadRequest)
            .WithSummary("Update an order")
            .WithDescription("Update an order");
        }
    }

    #endregion
}
