using BuildingBlocks.Pagination;
using Carter;
using Mapster;
using MediatR;
using Ordering.Application.DTOs;
using Ordering.Application.Orders.Queries.GetOrders;

namespace Ordering.Api.Endpoints
{
    #region Request

    public record GetOrdersRequest : BasePagination;

    #endregion

    #region Response

    public record GetOrdersResponse(PagedList<OrderDto> Orders);

    #endregion

    #region Endpoint

    public class GetOrders : ICarterModule
    {
        public void AddRoutes(IEndpointRouteBuilder app)
        {
            app.MapGet("/Orders", async ([AsParameters] GetOrdersRequest request, ISender sender) =>
            {
                // map request to query
                var query = request.Adapt<GetOrdersQuery>();

                // send query to handler and get result
                var result = await sender.Send(query);

                // map handler result to response
                var response = result.Adapt<GetOrdersResponse>();

                return Results.Ok(response);
            }).WithName("GetOrders")
            .Produces<GetOrdersResponse>(StatusCodes.Status200OK)
            .ProducesProblem(StatusCodes.Status400BadRequest)
            .WithSummary("Get orders")
            .WithDescription("Get orders");
        }
    }

    #endregion
}
