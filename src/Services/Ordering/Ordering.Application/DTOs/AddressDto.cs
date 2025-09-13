namespace Ordering.Application.DTOs
{
    public record AddressDto
    {
        public string FirstName { get; }
        public string LastName { get; }
        public string Email { get; }
        public string AddressLine { get; }
        public string City { get; }
        public string State { get; }
        public string Country { get; }
        public string ZipCode { get; }
    }
}
