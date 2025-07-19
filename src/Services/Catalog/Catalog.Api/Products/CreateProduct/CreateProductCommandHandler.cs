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

    #region Validator

    public class CreateProductValidator : AbstractValidator<CreateProductCommand>
    {
        public CreateProductValidator()
        {
            RuleFor(x => x.Name)
                .NotEmpty().WithMessage("Name is required")
                .Length(2, 300).WithMessage("Name must be between 2 and 300 characters");

            RuleFor(x => x.Price).GreaterThan(0).WithMessage("Price must be greater than 0");
            RuleFor(x => x.Image).NotEmpty().WithMessage("Image is required");
        }
    };

    #endregion

    #region Handler

    internal class CreateProductCommandHandler(IDocumentSession session) : ICommandHandler<CreateProductCommand, CreateProductResult>
    {
        public async Task<CreateProductResult> Handle(CreateProductCommand command, CancellationToken cancellationToken)
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
            session.Store(product);
            await session.SaveChangesAsync(cancellationToken);

            // 3. return CreateProductResult as result
            return new CreateProductResult(product.Id);
        }
    }

    #endregion
}
