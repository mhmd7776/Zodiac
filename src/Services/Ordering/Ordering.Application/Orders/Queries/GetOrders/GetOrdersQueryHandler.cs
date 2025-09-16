using BuildingBlocks.CQRS;
using BuildingBlocks.Pagination;
using Microsoft.EntityFrameworkCore;
using Ordering.Application.Data;
using Ordering.Application.DTOs;
using Ordering.Application.Extensions;

namespace Ordering.Application.Orders.Queries.GetOrders
{
    #region Query

    public record GetOrdersQuery : BasePagination, IQuery<GetOrdersResult>;

    #endregion

    #region Result

    public record GetOrdersResult(PagedList<OrderDto> Orders);

    #endregion

    #region Handler

    internal class GetOrdersQueryHandler(IOrderDbContext dbContext) : IQueryHandler<GetOrdersQuery, GetOrdersResult>
    {
        public async Task<GetOrdersResult> Handle(GetOrdersQuery query, CancellationToken cancellationToken)
        {
            // get orders
            var orders = await dbContext.Orders
                .Include(x => x.OrderItems)
                .AsNoTracking()
                .Skip(query.PageIndex * query.PageSize)
                .Take(query.PageSize)
                .ToListAsync(cancellationToken);

            // initial pagedlist
            var pagedList = new PagedList<OrderDto>
            {
                PageIndex = query.PageIndex,
                PageSize = query.PageSize,
                TotalCount = await dbContext.Orders.CountAsync(cancellationToken),
                Data = orders.ToOrderDto()
            };

            return new GetOrdersResult(pagedList);
        }
    }

    #endregion
}
