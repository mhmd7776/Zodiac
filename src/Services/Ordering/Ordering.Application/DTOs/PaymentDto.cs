namespace Ordering.Application.DTOs
{
    public record PaymentDto
    {
        public string CardName { get; }
        public string CardNumber { get; }
        public string ExpirationDate { get; }
        public string CVV2 { get; }
        public string PaymentMethod { get; }
    }
}
