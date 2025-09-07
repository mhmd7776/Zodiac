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

            await orderDbContext.Database.MigrateAsync();
        }
    }
}
