using BuildingBlocks.Exceptions.Handlers;
using Carter;
using HealthChecks.UI.Client;
using Microsoft.AspNetCore.Diagnostics.HealthChecks;

namespace Ordering.Api
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddApiServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddCarter();

            // add custom exception handling
            services.AddExceptionHandler<CustomExceptionHandler>();

            // cehck application health
            services.AddHealthChecks()
                .AddSqlServer(configuration.GetConnectionString("SqlServer") ?? string.Empty);

            return services;
        }

        public static WebApplication UseApiServices(this WebApplication app)
        {
            app.MapCarter();

            // custom exception handling
            app.UseExceptionHandler(options => { });

            // cehck application health
            app.UseHealthChecks("/Health", new HealthCheckOptions
            {
                ResponseWriter = UIResponseWriter.WriteHealthCheckUIResponse
            });

            return app;
        }
    }
}
