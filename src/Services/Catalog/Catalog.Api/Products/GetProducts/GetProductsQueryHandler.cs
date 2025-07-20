using BuildingBlocks.CQRS;
using Catalog.Api.Models;
using Marten.Pagination;

namespace Catalog.Api.Products.GetProducts
{
    #region Query

    public record GetProductsQuery(int PageNumber, int PageSize) : IQuery<GetProductsResult>;

    #endregion

    #region Result

    public record GetProductsResult(IEnumerable<Product> Products);

    #endregion

    #region Handler

    internal class GetProductsQueryHandler(IDocumentSession session) : IQueryHandler<GetProductsQuery, GetProductsResult>
    {
        public async Task<GetProductsResult> Handle(GetProductsQuery query, CancellationToken cancellationToken)
        {
            var products = await session.Query<Product>().ToPagedListAsync(query.PageNumber, query.PageSize, cancellationToken);
            return new GetProductsResult(products);
        }
    }

    #endregion
}
