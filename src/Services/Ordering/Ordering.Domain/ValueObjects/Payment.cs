namespace Ordering.Domain.ValueObjects
{
    public record Payment
    {
        internal Payment()
        {
            
        }

        private Payment(string cardName, string cardNumber, string expirationDate, string cVV2, string paymentMethod)
        {
            CardName = cardName;
            CardNumber = cardNumber;
            ExpirationDate = expirationDate;
            CVV2 = cVV2;
            PaymentMethod = paymentMethod;
        }

        public string CardName { get; } 
        public string CardNumber { get; }
        public string ExpirationDate { get; }
        public string CVV2 { get; }
        public string PaymentMethod { get; }

        public static Payment Of(string cardName, string cardNumber, string expirationDate, string cVV2, string paymentMethod)
        {
            ArgumentException.ThrowIfNullOrWhiteSpace(cardName);
            ArgumentException.ThrowIfNullOrWhiteSpace(cardNumber);
            ArgumentException.ThrowIfNullOrWhiteSpace(expirationDate);
            ArgumentException.ThrowIfNullOrWhiteSpace(cVV2);
            ArgumentOutOfRangeException.ThrowIfGreaterThan(cVV2.Length, 4);
            ArgumentException.ThrowIfNullOrWhiteSpace(paymentMethod);

            return new Payment(cardName, cardNumber, expirationDate, cVV2, paymentMethod);
        }
    }
}
