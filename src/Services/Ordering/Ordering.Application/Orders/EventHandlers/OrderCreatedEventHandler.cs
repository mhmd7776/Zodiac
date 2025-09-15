using MediatR;
using Ordering.Domain.Events;

namespace Ordering.Application.Orders.EventHandlers
{
    internal class OrderCreatedEventHandler : INotificationHandler<OrderCreatedEvent>
    {
        public Task Handle(OrderCreatedEvent notification, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}
