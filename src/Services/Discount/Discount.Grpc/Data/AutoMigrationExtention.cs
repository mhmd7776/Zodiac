using Microsoft.EntityFrameworkCore;

namespace Discount.Grpc.Data
{
    public static class AutoMigrationExtention
    {
        public static IApplicationBuilder AutoMigrate(this IApplicationBuilder applicationBuilder)
        {
            using var serviceScope = applicationBuilder.ApplicationServices.CreateScope();
            using var discountDbContext = serviceScope.ServiceProvider.GetRequiredService<DiscountDbContext>();

            discountDbContext.Database.MigrateAsync();

            return applicationBuilder;
        }
    }
}
