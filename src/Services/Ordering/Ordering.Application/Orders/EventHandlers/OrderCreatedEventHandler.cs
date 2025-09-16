﻿using MediatR;
using Ordering.Domain.Events;

namespace Ordering.Application.Orders.EventHandlers
{
    internal class OrderCreatedEventHandler : INotificationHandler<OrderCreatedEvent>
    {
        public Task Handle(OrderCreatedEvent notification, CancellationToken cancellationToken)
        {
            return Task.CompletedTask;
        }
    }
}
