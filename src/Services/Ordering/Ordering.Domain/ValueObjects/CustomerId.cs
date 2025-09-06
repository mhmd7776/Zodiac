using Ordering.Domain.Abstractions;
using Ordering.Domain.Exceptions;

namespace Ordering.Domain.ValueObjects
{
    public record CustomerId(Guid Value) : IStronglyTypeId<Guid>
    {
        public Guid Value { get; } = Value;

        public static CustomerId Of(Guid value)
        {
            ArgumentNullException.ThrowIfNull(value);
            if (value == Guid.Empty)
                throw new DomainException("Customer id can not be empty");

            return new CustomerId(value);
        }
    }
}