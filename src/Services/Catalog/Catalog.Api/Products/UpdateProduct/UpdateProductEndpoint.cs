using Catalog.Api.Products.CreateProduct;

namespace Catalog.Api.Products.UpdateProduct
{
    #region Request

    public record UpdateProductRequest
    {
        public Guid Id { get; set; }
        public string? Name { get; set; }
        public decimal Price { get; set; }
        public string? Description { get; set; }
        public string? Image { get; set; }
        public List<string> Categories { get; set; } = [];
    }

    #endregion

    #region Response

    public record UpdateProductResponse();

    #endregion

    #region Endpoint

    public class UpdateProductEndpoint : ICarterModule
    {
        public void AddRoutes(IEndpointRouteBuilder app)
        {
            app.MapPut("/Products", async (UpdateProductRequest request, ISender sender) =>
            {
                // map request to command
                var command = request.Adapt<UpdateProductCommand>();

                // send command to handler and get result
                var result = await sender.Send(command);

                return Results.NoContent();
            }).WithName("UpdateProduct")
            .Produces(StatusCodes.Status204NoContent)
            .ProducesProblem(StatusCodes.Status400BadRequest)
            .WithSummary("Update a product")
            .WithDescription("Update a product");
        }
    }

    #endregion
}
