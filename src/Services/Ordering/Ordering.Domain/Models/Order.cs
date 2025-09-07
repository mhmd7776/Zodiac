using Ordering.Domain.Abstractions;
using Ordering.Domain.Enums;
using Ordering.Domain.Events;
using Ordering.Domain.ValueObjects;

namespace Ordering.Domain.Models
{
    public class Order : Aggregate<OrderId>
    {
        private readonly List<OrderItem> _orderItems = [];
        public IReadOnlyList<OrderItem> OrderItems => _orderItems.AsReadOnly();

        public CustomerId CustomerId { get; private set; }

        public Address Address { get; private set; }
        public Payment Payment { get; private set; }
        public OrderStatus Status { get; private set; } = OrderStatus.Pending;

        public decimal OrderTotal
        {
            get => OrderItems.Sum(x => x.Price * x.Quantity);
            private set { }
        }

        public static Order Create(OrderId id, CustomerId customerId, Address address, Payment payment)
        {
            // initial order
            var order = new Order
            {
                Id = id,
                CustomerId = customerId,
                Address = address,
                Payment = payment
            };

            // add order event
            order.AddDomainEvent(new OrderCreatedEvent(order));

            return order;
        }

        public void Update(CustomerId customerId, Address address, Payment payment, OrderStatus status)
        {
            // update order
            CustomerId = customerId;
            Address = address;
            Payment = payment;
            Status = status;

            // update order event
            this.AddDomainEvent(new OrderUpdatedEvent(this));
        }

        public void AddItem(ProductId productId, decimal price, int quantity)
        {
            ArgumentOutOfRangeException.ThrowIfNegativeOrZero(price);
            ArgumentOutOfRangeException.ThrowIfNegativeOrZero(quantity);

            // initial order item
            var orderItem = new OrderItem(Id, productId, price, quantity);

            // add order item
            _orderItems.Add(orderItem);
        }

        public void RemoveItem(ProductId productId)
        {
            // get order item by product id
            var orderItem = _orderItems.FirstOrDefault(x => x.ProductId == productId);
            if (orderItem is not null)
                _orderItems.Remove(orderItem);
        }
    }
}
