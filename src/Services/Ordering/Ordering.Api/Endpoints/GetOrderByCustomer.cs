using Carter;
using Mapster;
using MediatR;
using Ordering.Application.DTOs;
using Ordering.Application.Orders.Queries.GetOrderByCustomer;

namespace Ordering.Api.Endpoints
{
    #region Request

    public record GetOrderByCustomerRequest();

    #endregion

    #region Response

    public record GetOrderByCustomerResponse(IEnumerable<OrderDto> Orders);

    #endregion

    #region Endpoint

    public class GetOrderByCustomer : ICarterModule
    {
        public void AddRoutes(IEndpointRouteBuilder app)
        {
            app.MapGet("/Orders/Customer/{id}", async (Guid id, ISender sender) =>
            {
                // map request to query
                var query = new GetOrderByCustomerQuery(id);

                // send query to handler and get result
                var result = await sender.Send(query);

                // map handler result to response
                var response = result.Adapt<GetOrderByCustomerResponse>();

                return Results.Ok(response);
            }).WithName("GetOrderByCustomer")
            .Produces<GetOrderByCustomerResponse>(StatusCodes.Status200OK)
            .ProducesProblem(StatusCodes.Status400BadRequest)
            .WithSummary("Get order by customer")
            .WithDescription("Get order by customer");
        }
    }

    #endregion
}
