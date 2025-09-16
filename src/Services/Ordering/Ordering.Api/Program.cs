using Ordering.Api;
using Ordering.Application;
using Ordering.Infrastructure;
using Ordering.Infrastructure.Data.Extensions;

var builder = WebApplication.CreateBuilder(args);

// Add services
builder.Services
    .AddApplicationServices()
    .AddInfrastructureServices(builder.Configuration)
    .AddApiServices(builder.Configuration);

// Configure the HTTPS request pipeline
var app = builder.Build();

app.UseApiServices();

// initial db vs auto migration
if (app.Environment.IsDevelopment())
{
    await app.InitialiseDbAsync();
}

app.Run();
