using Catalog.Api.Models;
using Catalog.Api.Products.GetProducts;

namespace Catalog.Api.Products.GetProductById
{
    #region Request

    public record GetProductByIdRequest();

    #endregion

    #region Response

    public record GetProductByIdResponse(Product Product);

    #endregion

    #region Endpoint

    public class GetProductByIdEndpoint : ICarterModule
    {
        public void AddRoutes(IEndpointRouteBuilder app)
        {
            app.MapGet("/Products/{id}", async (Guid id, ISender sender) =>
            {
                // map request to query
                var query = new GetProductByIdQuery(id);

                // send query to handler and get result
                var result = await sender.Send(query);

                // map handler result to response
                var response = result.Adapt<GetProductByIdResponse>();

                return Results.Ok(response);
            }).WithName("GetProductById")
            .Produces<GetProductByIdResponse>(StatusCodes.Status200OK)
            .ProducesProblem(StatusCodes.Status400BadRequest)
            .WithSummary("Get product by id")
            .WithDescription("Get product by id");
        }
    }

    #endregion
}
