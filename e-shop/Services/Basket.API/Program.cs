using BuildingBlocs.Behaviors;
using BuildingBlocs.Exceptions.Handlers;
using HealthChecks.UI.Client;
using Microsoft.AspNetCore.Diagnostics.HealthChecks;
using System.Reflection;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
Assembly assembly = typeof(Program).Assembly;

builder.Services.AddCarter();

builder.Services.AddMediatR(config =>
{
    config.RegisterServicesFromAssembly(assembly);
    config.AddOpenBehavior(typeof(Validator<,>));
    config.AddOpenBehavior(typeof(Logger<,>));
});

builder.Services.AddValidatorsFromAssembly(assembly);

string postgresStringConnection = builder.Configuration.GetConnectionString("Database")!;
string redisStringConnection = builder.Configuration.GetConnectionString("Redis")!;

builder.Services.AddMarten(options =>
{
    options.Connection(postgresStringConnection);
    options.Schema.For<ShoppingCart>().Identity(x => x.UserName);
}).UseLightweightSessions();

builder.Services.AddScoped<IBasketRepository, BasketRepository>();
builder.Services.Decorate<IBasketRepository, CachedBasketRepository>();

builder.Services.AddStackExchangeRedisCache(options =>
{
    options.Configuration = redisStringConnection;
    //options.InstanceName = "Basket";
});

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
