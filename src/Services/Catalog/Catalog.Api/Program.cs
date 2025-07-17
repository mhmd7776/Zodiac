global using Carter;
global using Mapster;
global using MediatR;
global using Marten;

var builder = WebApplication.CreateBuilder(args);

// Add services
builder.Services.AddCarter(new DependencyContextAssemblyCatalog([typeof(Program).Assembly]));

builder.Services.AddMediatR(configuration =>
{
    configuration.RegisterServicesFromAssembly(typeof(Program).Assembly);
});

builder.Services.AddMarten(options =>
{
    options.Connection(builder.Configuration.GetConnectionString("PostgreSQL") ?? string.Empty);
}).UseLightweightSessions();

var app = builder.Build();

// Configure the HTTPS request pipeline
app.MapCarter();

app.Run();
