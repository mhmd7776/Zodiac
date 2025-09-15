using MediatR;
using Ordering.Domain.Events;

namespace Ordering.Application.Orders.EventHandlers
{
    internal class OrderUpdatedEventHandler : INotificationHandler<OrderUpdatedEvent>
    {
        public Task Handle(OrderUpdatedEvent notification, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}
