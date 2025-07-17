using BuildingBlocks.CQRS;
using Catalog.Api.Exceptions;
using Catalog.Api.Models;
using Catalog.Api.Products.GetProducts;

namespace Catalog.Api.Products.GetProductById
{
    #region Query

    public record GetProductByIdQuery(Guid Id) : IQuery<GetProductByIdResult>;

    #endregion

    #region Result

    public record GetProductByIdResult(Product Product);

    #endregion

    #region Handler

    internal class GetProductByIdQueryHamdler(IDocumentSession session, ILogger<GetProductByIdQueryHamdler> logger) : IQueryHandler<GetProductByIdQuery, GetProductByIdResult>
    {
        public async Task<GetProductByIdResult> Handle(GetProductByIdQuery query, CancellationToken cancellationToken)
        {
            logger.LogInformation($"GetProductByIdQueryHamdler.Handle called with: {query}");

            var product = await session.LoadAsync<Product>(query.Id, cancellationToken);
            if (product == null)
                throw new ProductNotFoundException(query.Id);

            return new GetProductByIdResult(product);
        }
    }

    #endregion
}
