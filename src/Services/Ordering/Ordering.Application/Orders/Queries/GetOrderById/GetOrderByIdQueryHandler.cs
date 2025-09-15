using BuildingBlocks.CQRS;
using BuildingBlocks.Exceptions;
using Microsoft.EntityFrameworkCore;
using Ordering.Application.Data;
using Ordering.Application.DTOs;
using Ordering.Application.Extensions;
using Ordering.Domain.Models;
using Ordering.Domain.ValueObjects;

namespace Ordering.Application.Orders.Queries.GetOrderById
{
    #region Query

    public record GetOrderByIdQuery(Guid Id) : IQuery<GetOrderByIdResult>;

    #endregion

    #region Result

    public record GetOrderByIdResult(OrderDto OrderDto);

    #endregion

    #region Handler

    internal class GetOrderByIdQueryHamdler(IOrderDbContext dbContext) : IQueryHandler<GetOrderByIdQuery, GetOrderByIdResult>
    {
        public async Task<GetOrderByIdResult> Handle(GetOrderByIdQuery query, CancellationToken cancellationToken)
        {
            // get order by id
            var order = await dbContext.Orders
                .Include(x => x.OrderItems)
                .AsNoTracking()
                .SingleOrDefaultAsync(x => x.Id == OrderId.Of(query.Id), cancellationToken);

            // check order exist
            if (order == null)
                throw new NotFoundException(nameof(Order), query.Id.ToString());

            // return result
            return new GetOrderByIdResult(order.ToOrderDto());
        }
    }

    #endregion
}
