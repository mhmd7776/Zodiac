namespace Ordering.Application.DTOs
{
    public record PaymentDto
    {
        public string CardName { get; set; }
        public string CardNumber { get; set; }
        public string ExpirationDate { get; set; }
        public string CVV2 { get; set; }
        public string PaymentMethod { get; set; }
    }
}
