using BuildingBlocks.CQRS;
using Catalog.Api.Models;

namespace Catalog.Api.Products.GetProducts
{
    #region Query

    public record GetProductsQuery() : IQuery<GetProductsResult>;

    #endregion

    #region Result

    public record GetProductsResult(IEnumerable<Product> Products);

    #endregion

    #region Handler

    internal class GetProductsQueryHandler(IDocumentSession session) : IQueryHandler<GetProductsQuery, GetProductsResult>
    {
        public async Task<GetProductsResult> Handle(GetProductsQuery query, CancellationToken cancellationToken)
        {
            var products = await session.Query<Product>().ToListAsync(cancellationToken);
            return new GetProductsResult(products);
        }
    }

    #endregion
}
