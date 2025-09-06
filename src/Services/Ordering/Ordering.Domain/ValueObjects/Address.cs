namespace Ordering.Domain.ValueObjects
{
    public record Address
    {
        internal Address()
        {
            
        }

        private Address(string firstName, string lastName, string email, string addressLine, string city, string state, string country, string zipCode)
        {
            FirstName = firstName;
            LastName = lastName;
            Email = email;
            AddressLine = addressLine;
            City = city;
            State = state;
            Country = country;
            ZipCode = zipCode;
        }

        public string FirstName { get; }
        public string LastName { get; }
        public string Email { get; }
        public string AddressLine { get; }
        public string City { get; }
        public string State { get; }
        public string Country { get; }
        public string ZipCode { get; }

        public static Address Of(string firstName, string lastName, string email, string addressLine, string city, string state, string country, string zipCode)
        {
            ArgumentException.ThrowIfNullOrWhiteSpace(firstName);
            ArgumentException.ThrowIfNullOrWhiteSpace(lastName);
            ArgumentException.ThrowIfNullOrWhiteSpace(email);
            ArgumentException.ThrowIfNullOrWhiteSpace(addressLine);
            ArgumentException.ThrowIfNullOrWhiteSpace(city);
            ArgumentException.ThrowIfNullOrWhiteSpace(state);
            ArgumentException.ThrowIfNullOrWhiteSpace(country);
            ArgumentException.ThrowIfNullOrWhiteSpace(zipCode);

            return new Address(firstName, lastName, email, addressLine, city, state, country, zipCode);
        }
    }
}
