namespace Catalog.Api.Products.CreateProduct
{
    #region Request

    public record CreateProductRequest
    {
        public string? Name { get; set; }
        public decimal Price { get; set; }
        public string? Description { get; set; }
        public string? Image { get; set; }
        public List<string> Categories { get; set; } = [];
    }

    #endregion

    #region Response

    public record CreateProductResponse(Guid Id);

    #endregion

    #region Endpoint

    public class CreateProductEndpoint : ICarterModule
    {
        public void AddRoutes(IEndpointRouteBuilder app)
        {
            app.MapPost("/Products", async (CreateProductRequest request, ISender sender) =>
            {
                // map request to command
                var command = request.Adapt<CreateProductCommand>();

                // send command to handler and get result
                var result = await sender.Send(command);

                // map handler result to response
                var response = result.Adapt<CreateProductResponse>();

                return Results.Created($"/Products/{response.Id}", response);
            }).WithName("CreateProduct")
            .Produces<CreateProductResponse>(StatusCodes.Status201Created)
            .ProducesProblem(StatusCodes.Status400BadRequest)
            .WithSummary("Create a product")
            .WithDescription("Create a product");
        }
    }

    #endregion
}
