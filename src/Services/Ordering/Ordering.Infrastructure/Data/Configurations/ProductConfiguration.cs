using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Ordering.Domain.Models;
using Ordering.Domain.ValueObjects;

namespace Ordering.Infrastructure.Data.Configurations
{
    public class ProductConfiguration : IEntityTypeConfiguration<Product>
    {
        public void Configure(EntityTypeBuilder<Product> builder)
        {
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id).HasConversion(
                productId => productId.Value,
                dbValue => ProductId.Of(dbValue));

            builder.Property(x => x.Name).HasMaxLength(300).IsRequired();
            builder.Property(x => x.Price).IsRequired();
        }
    }
}
