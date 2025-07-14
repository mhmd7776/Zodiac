using BuildingBlocks.CQRS;
using Catalog.Api.Models;

namespace Catalog.Api.Products.CreateProduct
{
    #region Command

    public record CreateProductCommand : ICommand<CreateProductResult>
    {
        public string? Name { get; set; }
        public decimal Price { get; set; }
        public string? Description { get; set; }
        public string? Image { get; set; }
        public List<string> Categories { get; set; } = [];
    }

    #endregion

    #region Result

    public record CreateProductResult(Guid Id);

    #endregion

    #region Handler

    internal class CreateProductCommandHandler : ICommandHandler<CreateProductCommand, CreateProductResult>
    {
        public Task<CreateProductResult> Handle(CreateProductCommand command, CancellationToken cancellationToken)
        {
            // 1. create product entity from command object
            var product = new Product
            {
                Name = command.Name,
                Price = command.Price,
                Description = command.Description,
                Image = command.Image,
                Categories = command.Categories
            };

            // 2. save product to db

            // 3. return CreateProductResult as result
            return Task.FromResult(new CreateProductResult(Guid.NewGuid()));
        }
    }

    #endregion
}
