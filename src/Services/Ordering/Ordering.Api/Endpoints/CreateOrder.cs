using Carter;
using Mapster;
using MediatR;
using Ordering.Application.DTOs;
using Ordering.Application.Orders.Commands.CreateOrder;

namespace Ordering.Api.Endpoints
{
    #region Request

    public record CreateOrderRequest(OrderDto OrderDto);

    #endregion

    #region Response

    public record CreateOrderResponse(Guid Id);

    #endregion

    #region Endpoint

    public class CreateOrder : ICarterModule
    {
        public void AddRoutes(IEndpointRouteBuilder app)
        {
            app.MapPost("/Orders", async (CreateOrderRequest request, ISender sender) =>
            {
                // map request to command
                var command = request.Adapt<CreateOrderCommand>();

                // send command to handler and get result
                var result = await sender.Send(command);

                // map handler result to response
                var response = result.Adapt<CreateOrderResponse>();

                return Results.Created($"/Orders/{response.Id}", response);
            }).WithName("CreateOrder")
            .Produces<CreateOrderResponse>(StatusCodes.Status201Created)
            .ProducesProblem(StatusCodes.Status400BadRequest)
            .WithSummary("Create an order")
            .WithDescription("Create an order");
        }
    }

    #endregion
}
