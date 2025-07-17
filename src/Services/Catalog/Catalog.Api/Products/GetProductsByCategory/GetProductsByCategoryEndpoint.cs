using Catalog.Api.Models;
using Catalog.Api.Products.GetProducts;

namespace Catalog.Api.Products.GetProductsByCategory
{
    #region Request

    public record GetProductsByCategoryRequest();

    #endregion

    #region Response

    public record GetProductsByCategoryResponse(IEnumerable<Product> Products);

    #endregion

    #region Endpoint

    public class GetProductsByCategoryEndpoint : ICarterModule
    {
        public void AddRoutes(IEndpointRouteBuilder app)
        {
            app.MapGet("/Products/Category/{category}", async (string category, ISender sender) =>
            {
                // map request to query
                var query = new GetProductsByCategoryQuery(category);

                // send query to handler and get result
                var result = await sender.Send(query);

                // map handler result to response
                var response = result.Adapt<GetProductsByCategoryResponse>();

                return Results.Ok(response);
            }).WithName("GetProductsByCategory")
            .Produces<GetProductsByCategoryResponse>(StatusCodes.Status200OK)
            .ProducesProblem(StatusCodes.Status400BadRequest)
            .WithSummary("Get products by category")
            .WithDescription("Get products by category");
        }
    }

    #endregion
}
