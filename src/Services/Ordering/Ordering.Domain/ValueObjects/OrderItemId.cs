using Ordering.Domain.Abstractions;
using Ordering.Domain.Exceptions;

namespace Ordering.Domain.ValueObjects
{
    public record OrderItemId(Guid Value) : IStronglyTypeId<Guid>
    {
        public Guid Value { get; } = Value;

        public static OrderItemId Of(Guid value)
        {
            ArgumentNullException.ThrowIfNull(value);
            if (value == Guid.Empty)
                throw new DomainException("Order item id can not be empty");

            return new OrderItemId(value);
        }
    }
}
