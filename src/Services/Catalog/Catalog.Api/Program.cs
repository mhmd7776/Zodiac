global using Carter;
global using FluentValidation;
global using Mapster;
global using Marten;
global using MediatR;
using BuildingBlocks.Behaviors;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using System.Net.Mime;

var builder = WebApplication.CreateBuilder(args);

// Add services
builder.Services.AddCarter(new DependencyContextAssemblyCatalog([typeof(Program).Assembly]));
builder.Services.AddValidatorsFromAssembly(typeof(Program).Assembly);

builder.Services.AddMediatR(configuration =>
{
    configuration.RegisterServicesFromAssembly(typeof(Program).Assembly);
    configuration.AddOpenBehaviors([typeof(ValidationBehavior<,>)]);
});

builder.Services.AddMarten(options =>
{
    options.Connection(builder.Configuration.GetConnectionString("PostgreSQL") ?? string.Empty);
}).UseLightweightSessions();

var app = builder.Build();

// Configure the HTTPS request pipeline
app.MapCarter();

app.UseExceptionHandler(applicationBuilder =>
{
    applicationBuilder.Run(async httpContext =>
    {
        var exception = httpContext.Features.Get<IExceptionHandlerFeature>()?.Error;
        if (exception is null)
            return;
        
        var problemDetails = new ProblemDetails
        {
            Title = exception.Message,
            Status = StatusCodes.Status500InternalServerError,
            Detail = exception.StackTrace
        };

        var logger = httpContext.RequestServices.GetRequiredService<ILogger<Program>>();
        logger.LogError(exception, exception.Message);

        httpContext.Response.StatusCode = StatusCodes.Status500InternalServerError;
        httpContext.Response.ContentType = MediaTypeNames.Application.ProblemJson;

        await httpContext.Response.WriteAsJsonAsync(problemDetails);
    });
});

app.Run();
