using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace Ordering.Infrastructure.Data.Extensions
{
    public static class DbExtensions
    {
        public static async Task InitialiseDbAsync(this WebApplication webApplication)
        {
            using var serviceScope = webApplication.Services.CreateScope();
            using var orderDbContext = serviceScope.ServiceProvider.GetRequiredService<OrderDbContext>();

            orderDbContext.Database.MigrateAsync().GetAwaiter().GetResult();

            // seed data
            await SeedDataAsync(orderDbContext);
        }

        private static async Task SeedDataAsync(OrderDbContext orderDbContext)
        {
            // add customers if there is no one 
            if (!await orderDbContext.Customers.AnyAsync())
            {
                await orderDbContext.Customers.AddRangeAsync(InitialData.Customers);
                await orderDbContext.SaveChangesAsync();
            }

            // add products if there is no one 
            if (!await orderDbContext.Products.AnyAsync())
            {
                await orderDbContext.Products.AddRangeAsync(InitialData.Products);
                await orderDbContext.SaveChangesAsync();
            }

            // add orders with items if there is no one 
            if (!await orderDbContext.Orders.AnyAsync())
            {
                await orderDbContext.Orders.AddRangeAsync(InitialData.OrdersWithItems);
                await orderDbContext.SaveChangesAsync();
            }
        }
    }
}
