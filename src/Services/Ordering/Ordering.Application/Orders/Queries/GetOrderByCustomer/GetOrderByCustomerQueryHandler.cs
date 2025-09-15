using BuildingBlocks.CQRS;
using BuildingBlocks.Exceptions;
using Microsoft.EntityFrameworkCore;
using Ordering.Application.Data;
using Ordering.Application.DTOs;
using Ordering.Application.Extensions;
using Ordering.Domain.Models;
using Ordering.Domain.ValueObjects;

namespace Ordering.Application.Orders.Queries.GetOrderByCustomer
{
    #region Query

    public record GetOrderByCustomerQuery(Guid CustomerId) : IQuery<GetOrderByCustomerResult>;

    #endregion

    #region Result

    public record GetOrderByCustomerResult(IEnumerable<OrderDto> Orders);

    #endregion

    #region Handler

    internal class GetOrderByCustomerQueryHamdler(IOrderDbContext dbContext) : IQueryHandler<GetOrderByCustomerQuery, GetOrderByCustomerResult>
    {
        public async Task<GetOrderByCustomerResult> Handle(GetOrderByCustomerQuery query, CancellationToken cancellationToken)
        {
            // get orders by customer
            var orders = await dbContext.Orders
                .Include(x => x.OrderItems)
                .AsNoTracking()
                .Where(x => x.CustomerId == CustomerId.Of(query.CustomerId))
                .ToListAsync(cancellationToken);

            // return result
            return new GetOrderByCustomerResult(orders.ToOrderDto());
        }
    }

    #endregion
}
