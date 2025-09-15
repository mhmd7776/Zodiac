namespace BuildingBlocks.Exceptions
{
    public class NotFoundException : Exception
    {
        public NotFoundException(string message) : base(message) { }

        public NotFoundException(string entity, string key) : base($"The {entity} with key {key} is not found") { }
    }
}
