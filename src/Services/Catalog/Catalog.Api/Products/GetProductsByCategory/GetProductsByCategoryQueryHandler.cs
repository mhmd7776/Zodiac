using BuildingBlocks.CQRS;
using Catalog.Api.Models;
using Catalog.Api.Products.GetProducts;

namespace Catalog.Api.Products.GetProductsByCategory
{
    #region Query

    public record GetProductsByCategoryQuery(string Category) : IQuery<GetProductsByCategoryResult>;

    #endregion

    #region Result

    public record GetProductsByCategoryResult(IEnumerable<Product> Products);

    #endregion

    #region Handler

    internal class GetProductsByCategoryQueryHandler(IDocumentSession session) : IQueryHandler<GetProductsByCategoryQuery, GetProductsByCategoryResult>
    {
        public async Task<GetProductsByCategoryResult> Handle(GetProductsByCategoryQuery query, CancellationToken cancellationToken)
        {
            var products = await session.Query<Product>()
                .Where(x => x.Categories.Contains(query.Category))
                .ToListAsync(cancellationToken);

            return new GetProductsByCategoryResult(products);
        }
    }

    #endregion
}
