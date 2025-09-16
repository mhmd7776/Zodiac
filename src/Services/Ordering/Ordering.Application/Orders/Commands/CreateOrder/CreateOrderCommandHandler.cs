using BuildingBlocks.CQRS;
using FluentValidation;
using Ordering.Application.Data;
using Ordering.Application.DTOs;
using Ordering.Domain.Models;
using Ordering.Domain.ValueObjects;

namespace Ordering.Application.Orders.Commands.CreateOrder
{
    #region Command

    public record CreateOrderCommand(OrderDto OrderDto) : ICommand<CreateOrderResult>;

    #endregion

    #region Result

    public record CreateOrderResult(Guid Id);

    #endregion

    #region Validator

    public class CreateOrderValidator : AbstractValidator<CreateOrderCommand>
    {
        public CreateOrderValidator()
        {
            RuleFor(x => x.OrderDto.CustomerId).NotNull().WithMessage("Customer id can not be null");
            RuleFor(x => x.OrderDto.Address).NotNull().WithMessage("Address can not be null");
            RuleFor(x => x.OrderDto.Payment).NotNull().WithMessage("Payment can not be null");
            RuleFor(x => x.OrderDto.OrderItems).NotEmpty().WithMessage("Items can not be empty");
        }
    };

    #endregion

    #region Handler

    internal class CreateOrderCommandHandler(IOrderDbContext dbContext) : ICommandHandler<CreateOrderCommand, CreateOrderResult>
    {
        public async Task<CreateOrderResult> Handle(CreateOrderCommand command, CancellationToken cancellationToken)
        {
            // 1. create order from command object
            var order = CreateOrder(command.OrderDto);

            // 2. save order to db
            await dbContext.Orders.AddAsync(order, cancellationToken);
            await dbContext.SaveChangesAsync(cancellationToken);

            // 3. return CreateProductResult as result
            return new CreateOrderResult(order.Id.Value);
        }

        private static Order CreateOrder(OrderDto orderDto)
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

            var order = Order.Create(
                OrderId.Of(Guid.NewGuid()),
                CustomerId.Of(orderDto.CustomerId),
                address,
                payment);

            // add order items
            foreach (var orderItem in orderDto.OrderItems)
            {
                order.AddItem(
                    ProductId.Of(orderItem.ProductId),
                    orderItem.Price,
                    orderItem.Quantity);
            }

            return order;
        }
    }

    #endregion
}
