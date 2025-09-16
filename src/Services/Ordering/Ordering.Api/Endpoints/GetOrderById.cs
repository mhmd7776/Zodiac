using Carter;
using Mapster;
using MediatR;
using Ordering.Application.DTOs;
using Ordering.Application.Orders.Queries.GetOrderById;

namespace Ordering.Api.Endpoints
{
    #region Request

    public record GetOrderByIdRequest();

    #endregion

    #region Response

    public record GetOrderByIdResponse(OrderDto OrderDto);

    #endregion

    #region Endpoint

    public class GetOrderById : ICarterModule
    {
        public void AddRoutes(IEndpointRouteBuilder app)
        {
            app.MapGet("/Orders/{id}", async (Guid id, ISender sender) =>
            {
                // map request to query
                var query = new GetOrderByIdQuery(id);

                // send query to handler and get result
                var result = await sender.Send(query);

                // map handler result to response
                var response = result.Adapt<GetOrderByIdResponse>();

                return Results.Ok(response);
            }).WithName("GetOrderById")
            .Produces<GetOrderByIdResponse>(StatusCodes.Status200OK)
            .ProducesProblem(StatusCodes.Status400BadRequest)
            .WithSummary("Get order by id")
            .WithDescription("Get order by id");
        }
    }

    #endregion
}
