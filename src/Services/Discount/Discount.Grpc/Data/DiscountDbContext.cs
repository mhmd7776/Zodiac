using Discount.Grpc.Models;
using Microsoft.EntityFrameworkCore;

namespace Discount.Grpc.Data
{
    public class DiscountDbContext(DbContextOptions<DiscountDbContext> options) : DbContext(options)
    {
        public DbSet<Coupon> Coupons { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            var coupons = new List<Coupon>
            {
                new() {
                    Id = 1,
                    ProductId = new Guid("019818ea-8772-4018-b80e-299a71c88593"),
                    Amount = 10,
                    IsPercentage = true
                },
                new() {
                    Id = 2,
                    ProductId = new Guid("019818ed-1b45-4d55-bdca-2af7a00883ff"),
                    Amount = 49,
                    IsPercentage = false
                },
                new() {
                    Id = 3,
                    ProductId = new Guid("5334c996-8457-4cf0-815c-ed2b77c4ff61"),
                    Description = "Special coupon that can only be used once",
                    Amount = 129,
                    IsPercentage = false
                }
            };

            modelBuilder.Entity<Coupon>().HasData(coupons);

            base.OnModelCreating(modelBuilder);
        }
    }
}
