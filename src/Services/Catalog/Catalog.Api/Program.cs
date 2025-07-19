global using Carter;
global using FluentValidation;
global using Mapster;
global using Marten;
global using MediatR;
using BuildingBlocks.Behaviors;
using BuildingBlocks.Exceptions.Handlers;

var builder = WebApplication.CreateBuilder(args);

// Add services
builder.Services.AddCarter(new DependencyContextAssemblyCatalog([typeof(Program).Assembly]));
builder.Services.AddValidatorsFromAssembly(typeof(Program).Assembly);

builder.Services.AddMediatR(configuration =>
{
    configuration.RegisterServicesFromAssembly(typeof(Program).Assembly);
    configuration.AddOpenBehaviors([typeof(ValidationBehavior<,>), typeof(LoggingBehavior<,>)]);
});

builder.Services.AddMarten(options =>
{
    options.Connection(builder.Configuration.GetConnectionString("PostgreSQL") ?? string.Empty);
}).UseLightweightSessions();

builder.Services.AddExceptionHandler<CustomExceptionHandler>();

var app = builder.Build();

// Configure the HTTPS request pipeline
app.MapCarter();

app.UseExceptionHandler(options => { });

app.Run();
