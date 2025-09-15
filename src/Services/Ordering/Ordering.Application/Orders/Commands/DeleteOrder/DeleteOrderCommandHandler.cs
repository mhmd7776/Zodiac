using BuildingBlocks.CQRS;
using BuildingBlocks.Exceptions;
using FluentValidation;
using MediatR;
using Ordering.Application.Data;
using Ordering.Domain.Models;
using Ordering.Domain.ValueObjects;

namespace Ordering.Application.Orders.Commands.DeleteOrder
{
    #region Command

    public record DeleteOrderCommand(Guid Id) : ICommand;

    #endregion

    #region Result

    public record DeleteOrderResult();

    #endregion

    #region Validator

    public class DeleteOrderValidator : AbstractValidator<DeleteOrderCommand>
    {
        public DeleteOrderValidator()
        {
            RuleFor(x => x.Id).NotEmpty().WithMessage("Id can not be empty");
        }
    };

    #endregion

    #region Handler

    internal class DeleteOrderCommandHandler(IOrderDbContext dbContext) : ICommandHandler<DeleteOrderCommand>
    {
        public async Task<Unit> Handle(DeleteOrderCommand command, CancellationToken cancellationToken)
        {
            // get and validate order
            var orderId = OrderId.Of(command.Id);
            var order = await dbContext.Orders.FindAsync([orderId], cancellationToken: cancellationToken) ?? throw new NotFoundException(nameof(Order), orderId.Value.ToString());

            // delete order
            dbContext.Orders.Remove(order);
            await dbContext.SaveChangesAsync(cancellationToken);

            // return result
            return Unit.Value;
        }
    }

    #endregion
}
