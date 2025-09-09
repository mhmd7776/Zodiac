namespace Ordering.Domain.Abstractions
{
    public interface IEntity
    {
        public DateTime? CreatedOnUtc { get; set; }
        public DateTime? UpdatedOnUtc { get; set; }
    }

    public interface IEntity<T> : IEntity
    {
        public T Id { get; set; }
    }
}
