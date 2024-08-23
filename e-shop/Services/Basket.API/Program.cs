using Basket.API;
using BuildingBlocs.Exceptions.Handlers;
using HealthChecks.UI.Client;
using Microsoft.AspNetCore.Diagnostics.HealthChecks;
using System.Reflection;

WebApplicationBuilder builder = WebApplication.CreateBuilder(args);

Assembly assembly = typeof(Program).Assembly;

builder.Services.AddCarter();

builder.ConfigureMediatR(assembly);

builder.Services.AddValidatorsFromAssembly(assembly);

string postgresStringConnection = builder.Configuration.GetConnectionString("Database")!;
builder.ConfigureMarten(postgresStringConnection);

builder.ConfigureRepos();

string redisStringConnection = builder.Configuration.GetConnectionString("Redis")!;
builder.ConfigureRedis(redisStringConnection);

builder.Services.AddHealthChecks()
    .AddNpgSql(postgresStringConnection)
    .AddRedis(redisStringConnection);

builder.Services.AddExceptionHandler<CustomExceptionHandler>();






var app = builder.Build();

// Configure the HTTP request pipeline.
app.MapCarter();
app.UseExceptionHandler(options => { });
app.UseHealthChecks("/health",
    new HealthCheckOptions
    {
        ResponseWriter = UIResponseWriter.WriteHealthCheckUIResponse
    }
);

app.Run();
