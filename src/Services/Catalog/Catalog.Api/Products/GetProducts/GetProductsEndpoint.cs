using Catalog.Api.Models;
using Catalog.Api.Products.CreateProduct;

namespace Catalog.Api.Products.GetProducts
{
    #region Request

    public record GetProductsRequest();

    #endregion

    #region Response

    public record GetProductsResponse(IEnumerable<Product> Products);

    #endregion

    #region Endpoint

    public class GetProductsEndpoint : ICarterModule
    {
        public void AddRoutes(IEndpointRouteBuilder app)
        {
            app.MapGet("/Products", async (ISender sender) =>
            {
                // map request to query
                var query = new GetProductsQuery();

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
