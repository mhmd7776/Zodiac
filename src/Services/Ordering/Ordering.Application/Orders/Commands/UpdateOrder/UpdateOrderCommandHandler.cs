using BuildingBlocks.CQRS;
using BuildingBlocks.Exceptions;
using FluentValidation;
using MediatR;
using Ordering.Application.Data;
using Ordering.Application.DTOs;
using Ordering.Domain.Models;
using Ordering.Domain.ValueObjects;

namespace Ordering.Application.Orders.Commands.UpdateOrder
{
    #region Command

    public record UpdateOrderCommand(OrderDto OrderDto) : ICommand;

    #endregion

    #region Result

    public record UpdateOrderResult();

    #endregion

    #region Validator

    public class UpdateOrderValidator : AbstractValidator<UpdateOrderCommand>
    {
        public UpdateOrderValidator()
        {
            RuleFor(x => x.OrderDto.Id).NotEmpty().WithMessage("Id can not be empty");
            RuleFor(x => x.OrderDto.CustomerId).NotNull().WithMessage("Customer id can not be null");
            RuleFor(x => x.OrderDto.Address).NotNull().WithMessage("Address can not be null");
            RuleFor(x => x.OrderDto.Payment).NotNull().WithMessage("Payment can not be null");
        }
    };

    #endregion

    #region Handler

    internal class UpdateOrderCommandHandler(IOrderDbContext dbContext) : ICommandHandler<UpdateOrderCommand>
    {
        public async Task<Unit> Handle(UpdateOrderCommand command, CancellationToken cancellationToken)
        {
            // get and validate order
            var orderId = OrderId.Of(command.OrderDto.Id);
            var order = await dbContext.Orders.FindAsync([orderId], cancellationToken: cancellationToken) ?? throw new NotFoundException(nameof(Order), orderId.Value.ToString());

            // update order
            UpdateOrder(order, command.OrderDto);

            // return result
            return Unit.Value;
        }

        private static void UpdateOrder(Order order, OrderDto orderDto)
        {
            var address = Address.Of(
                orderDto.Address.FirstName,
                orderDto.Address.LastName,
                orderDto.Address.Email,
                orderDto.Address.AddressLine,
                orderDto.Address.City,
                orderDto.Address.State,
                orderDto.Address.Country,
                orderDto.Address.ZipCode);

            var payment = Payment.Of(
                orderDto.Payment.CardName,
                orderDto.Payment.CardNumber,
                orderDto.Payment.ExpirationDate,
                orderDto.Payment.CVV2,
                orderDto.Payment.PaymentMethod);

            order.Update(
                CustomerId.Of(orderDto.CustomerId),
                address,
                payment,
                orderDto.Status);
        }
    }

    #endregion
}
