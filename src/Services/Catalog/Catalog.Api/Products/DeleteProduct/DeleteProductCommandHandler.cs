using BuildingBlocks.CQRS;
using Catalog.Api.Exceptions;
using Catalog.Api.Models;
using JasperFx.Events.Daemon;

namespace Catalog.Api.Products.DeleteProduct
{
    #region Command

    public record DeleteProductCommand(Guid Id) : ICommand;

    #endregion

    #region Result

    public record DeleteProductResult();

    #endregion

    #region Handler

    internal class DeleteProductCommandHandler(IDocumentSession session, ILogger<DeleteProductCommandHandler> logger) : ICommandHandler<DeleteProductCommand>
    {
        public async Task<Unit> Handle(DeleteProductCommand command, CancellationToken cancellationToken)
        {
            logger.LogInformation($"DeleteProductCommandHandler.Handle called with: {command}");

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
