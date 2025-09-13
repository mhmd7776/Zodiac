namespace Ordering.Application.DTOs
{
    public record OrderItemDto
    {
        public Guid Id { get; set; }
        public Guid OrderId { get; private set; }
        public Guid ProductId { get; private set; }
        public decimal Price { get; private set; }
        public int Quantity { get; private set; }
    }
}
