namespace Ordering.Domain.Abstractions
{
    public interface IStronglyTypeId<T>
    {
        public T Value { get; }
    }
}
