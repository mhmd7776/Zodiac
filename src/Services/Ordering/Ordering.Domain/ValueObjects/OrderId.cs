using Ordering.Domain.Abstractions;
using Ordering.Domain.Exceptions;

namespace Ordering.Domain.ValueObjects
{
    public record OrderId(Guid Value) : IStronglyTypeId<Guid>
    {
        public Guid Value { get; } = Value;

        public static OrderId Of(Guid value)
        {
            ArgumentNullException.ThrowIfNull(value);
            if (value == Guid.Empty)
                throw new DomainException("Order id can not be empty");

            return new OrderId(value);
        }
    }
}
