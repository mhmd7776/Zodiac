using Catalog.Api.Products.CreateProduct;
using Catalog.Api.Products.UpdateProduct;

namespace Catalog.Api.Products.DeleteProduct
{
    #region Request

    public record DeleteProductRequest();

    #endregion

    #region Response

    public record DeleteProductResponse();

    #endregion

    #region Endpoint

    public class DeleteProductEndpoint : ICarterModule
    {
        public void AddRoutes(IEndpointRouteBuilder app)
        {
            app.MapDelete("/Products/{id}", async (Guid id, ISender sender) =>
            {
                // map request to command
                var command = new DeleteProductCommand(id);

                // send command to handler and get result
                var result = await sender.Send(command);

                return Results.Ok();
            }).WithName("DeleteProduct")
            .Produces(StatusCodes.Status200OK)
            .ProducesProblem(StatusCodes.Status400BadRequest)
            .WithSummary("Delete a product")
            .WithDescription("Delete a product");
        }
    }

    #endregion
}
