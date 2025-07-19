using BuildingBlocks.CQRS;
using Catalog.Api.Exceptions;
using Catalog.Api.Models;
using Catalog.Api.Products.CreateProduct;

namespace Catalog.Api.Products.UpdateProduct
{
    #region Command

    public record UpdateProductCommand : ICommand
    {
        public Guid Id { get; set; }
        public string? Name { get; set; }
        public decimal Price { get; set; }
        public string? Description { get; set; }
        public string? Image { get; set; }
        public List<string> Categories { get; set; } = [];
    }

    #endregion

    #region Result

    public record UpdateProductResult();

    #endregion

    #region Validator

    public class UpdateProductValidator : AbstractValidator<UpdateProductCommand>
    {
        public UpdateProductValidator()
        {
            RuleFor(x => x.Name)
                .NotEmpty().WithMessage("Name is required")
                .Length(2, 300).WithMessage("Name must be between 2 and 300 characters");

            RuleFor(x => x.Id).NotEmpty().WithMessage("Id is required");
            RuleFor(x => x.Price).GreaterThan(0).WithMessage("Price must be greater than 0");
            RuleFor(x => x.Image).NotEmpty().WithMessage("Image is required");
        }
    };

    #endregion

    #region Handler

    internal class UpdateProductCommandHandler(IDocumentSession session, ILogger<UpdateProductCommandHandler> logger) : ICommandHandler<UpdateProductCommand>
    {
        public async Task<Unit> Handle(UpdateProductCommand command, CancellationToken cancellationToken)
        {
            logger.LogInformation($"UpdateProductCommandHandler.Handle called with: {command}");

            // get product by id
            var product = await session.LoadAsync<Product>(command.Id, cancellationToken);
            if (product is null)
                throw new ProductNotFoundException(command.Id);

            // update product
            product.Name = command.Name;
            product.Price = command.Price;
            product.Description = command.Description;
            product.Image = command.Image;
            product.Categories = command.Categories;

            session.Update(product);
            await session.SaveChangesAsync(cancellationToken);

            return Unit.Value;
        }
    }

    #endregion
}
