using Catalog.Api.Models;
using Marten.Pagination;

namespace Catalog.Api.Products.GetProducts
{
    #region Request

    public record GetProductsRequest(int? PageNumber = 1, int? PageSize = 10);

    #endregion

    #region Response

    public record GetProductsResponse(IEnumerable<Product> Products);

    #endregion

    #region Endpoint

    public class GetProductsEndpoint : ICarterModule
    {
        public void AddRoutes(IEndpointRouteBuilder app)
        {
            app.MapGet("/Products", async ([AsParameters] GetProductsRequest request, ISender sender) =>
            {
                // map request to query
                var query = request.Adapt<GetProductsQuery>();

                // send query to handler and get result
                var result = await sender.Send(query);

                // map handler result to response
                var response = result.Adapt<GetProductsResponse>();

                return Results.Ok(response);
            }).WithName("GetProducts")
            .Produces<GetProductsResponse>(StatusCodes.Status200OK)
            .ProducesProblem(StatusCodes.Status400BadRequest)
            .WithSummary("Get products")
            .WithDescription("Get products");
        }
    }

    #endregion
}
