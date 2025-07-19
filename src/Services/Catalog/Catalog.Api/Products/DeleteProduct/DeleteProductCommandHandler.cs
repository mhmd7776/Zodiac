using BuildingBlocks.CQRS;
using Catalog.Api.Exceptions;
using Catalog.Api.Models;
using Catalog.Api.Products.CreateProduct;
using JasperFx.Events.Daemon;

namespace Catalog.Api.Products.DeleteProduct
{
    #region Command

    public record DeleteProductCommand(Guid Id) : ICommand;

    #endregion

    #region Result

    public record DeleteProductResult();

    #endregion

    #region Validator

    public class DeleteProductValidator : AbstractValidator<DeleteProductCommand>
    {
        public DeleteProductValidator()
        {
            RuleFor(x => x.Id).NotEmpty().WithMessage("Id is required");
        }
    };

    #endregion

    #region Handler

    internal class DeleteProductCommandHandler(IDocumentSession session) : ICommandHandler<DeleteProductCommand>
    {
        public async Task<Unit> Handle(DeleteProductCommand command, CancellationToken cancellationToken)
        {
            // get product by id
            var product = await session.LoadAsync<Product>(command.Id, cancellationToken);
            if (product is null)
                throw new ProductNotFoundException(command.Id);

            session.Delete(product);
            await session.SaveChangesAsync(cancellationToken);

            return Unit.Value;
        }
    }

    #endregion
}
