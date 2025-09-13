using Ordering.Domain.Enums;

namespace Ordering.Application.DTOs
{
    public record OrderDto
    {
        public Guid Id { get; set; }
        public Guid CustomerId { get; set; }
        public AddressDto Address { get; set; }
        public PaymentDto Payment { get; set; }
        public OrderStatus Status { get; set; }
        public List<OrderItemDto> OrderItems { get; set; } = [];
    }
}
