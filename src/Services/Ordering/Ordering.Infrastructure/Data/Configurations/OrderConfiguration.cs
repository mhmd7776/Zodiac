using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Ordering.Domain.Enums;
using Ordering.Domain.Models;
using Ordering.Domain.ValueObjects;

namespace Ordering.Infrastructure.Data.Configurations
{
    public class OrderConfiguration : IEntityTypeConfiguration<Order>
    {
        public void Configure(EntityTypeBuilder<Order> builder)
        {
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id).HasConversion(
                orderId => orderId.Value,
                dbValue => OrderId.Of(dbValue));

            builder.HasOne<Customer>()
                .WithMany()
                .HasForeignKey(x => x.CustomerId)
                .IsRequired();

            builder.HasMany(x => x.OrderItems)
                .WithOne()
                .HasForeignKey(x => x.OrderId)
                .IsRequired();

            builder.ComplexProperty(x => x.Address, addressBuilder =>
            {
                addressBuilder.IsRequired();
                addressBuilder.Property(_ => _.FirstName).HasMaxLength(150).IsRequired();
                addressBuilder.Property(_ => _.LastName).HasMaxLength(150).IsRequired();
                addressBuilder.Property(_ => _.Email).HasMaxLength(300).IsRequired();
                addressBuilder.Property(_ => _.AddressLine).HasMaxLength(300).IsRequired();
                addressBuilder.Property(_ => _.City).HasMaxLength(100).IsRequired();
                addressBuilder.Property(_ => _.State).HasMaxLength(100).IsRequired();
                addressBuilder.Property(_ => _.Country).HasMaxLength(100).IsRequired();
                addressBuilder.Property(_ => _.ZipCode).HasMaxLength(50).IsRequired();
            });

            builder.ComplexProperty(x => x.Payment, paymentBuilder =>
            {
                paymentBuilder.IsRequired();
                paymentBuilder.Property(_ => _.CardName).HasMaxLength(100).IsRequired();
                paymentBuilder.Property(_ => _.CardNumber).HasMaxLength(50).IsRequired();
                paymentBuilder.Property(_ => _.ExpirationDate).HasMaxLength(50).IsRequired();
                paymentBuilder.Property(_ => _.CVV2).HasMaxLength(10).IsRequired();
                paymentBuilder.Property(_ => _.PaymentMethod).HasMaxLength(100).IsRequired();
            });

            builder.Property(x => x.Status).HasDefaultValue(OrderStatus.Pending).HasConversion(
                orderStatus => (int)orderStatus,
                dbStatus => (OrderStatus)dbStatus);

            builder.Property(x => x.OrderTotal).IsRequired();
        }
    }
}
