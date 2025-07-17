namespace Catalog.Api.Exceptions
{
    public class ProductNotFoundException(Guid id) : Exception(message: $"No product with ID {id} was found.")
    {
    }
}
