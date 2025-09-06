using Ordering.Domain.Abstractions;
using Ordering.Domain.Exceptions;

namespace Ordering.Domain.ValueObjects
{
    public record ProductId(Guid Value) : IStronglyTypeId<Guid>
    {
        public Guid Value { get; } = Value;

        public static ProductId Of(Guid value)
        {
            ArgumentNullException.ThrowIfNull(value);
            if (value == Guid.Empty)
                throw new DomainException("Product id can not be empty");

            return new ProductId(value);
        }
    }
}
