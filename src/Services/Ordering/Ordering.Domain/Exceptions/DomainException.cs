namespace Ordering.Domain.Exceptions
{
    public class DomainException(string message) : Exception($"Domain exception: \"{message}\" throws form domain layer.")
    {
    }
}
