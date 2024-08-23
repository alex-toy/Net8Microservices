using BuildingBlocs.Behaviors;
using BuildingBlocs.Exceptions.Handlers;
using Discount.Grpc;
using System.Reflection;

namespace Basket.API;

public static class WebApplicationBuilderExtensions
{
    public static void ConfigureApplicationServices(this WebApplicationBuilder builder)
    {
        Assembly assembly = typeof(Program).Assembly;
        builder.Services.AddCarter();
        builder.ConfigureMediatR(assembly);
    }

    public static void ConfigureDataServices(this WebApplicationBuilder builder, string postgresStringConnection, string redisStringConnection)
    {
        builder.ConfigureMarten(postgresStringConnection);
        builder.ConfigureRepos();
        builder.ConfigureRedis(redisStringConnection);
    }

    public static void ConfigureCrossCuttingServices(this WebApplicationBuilder builder, string postgresStringConnection, string redisStringConnection)
    {
        builder.Services.AddExceptionHandler<CustomExceptionHandler>();
        builder.Services.AddHealthChecks()
            .AddNpgSql(postgresStringConnection)
            .AddRedis(redisStringConnection);
    }

    public static void ConfigureGrpc(this WebApplicationBuilder builder)
    {
        builder.Services.AddGrpcClient<DiscountProtoService.DiscountProtoServiceClient>(options =>
        {
            options.Address = new Uri(builder.Configuration["GrpcSettings:DiscountUrl"]!);
        })
        .ConfigurePrimaryHttpMessageHandler(() =>
        {
            var handler = new HttpClientHandler
            {
                ServerCertificateCustomValidationCallback = HttpClientHandler.DangerousAcceptAnyServerCertificateValidator
            };

            return handler;
        });
    }

    private static void ConfigureMediatR(this WebApplicationBuilder builder, Assembly assembly)
    {
        builder.Services.AddMediatR(config =>
        {
            config.RegisterServicesFromAssembly(assembly);
            config.AddOpenBehavior(typeof(Validator<,>));
            config.AddOpenBehavior(typeof(Logger<,>));
        });
    }

    private static void ConfigureRepos(this WebApplicationBuilder builder)
    {
        builder.Services.AddScoped<IBasketRepository, BasketRepository>();
        builder.Services.Decorate<IBasketRepository, CachedBasketRepository>();
    }

    private static void ConfigureMarten(this WebApplicationBuilder builder, string postgresStringConnection)
    {
        builder.Services.AddMarten(options =>
        {
            options.Connection(postgresStringConnection);
            options.Schema.For<ShoppingCart>().Identity(x => x.UserName);
        }).UseLightweightSessions();
    }

    private static void ConfigureRedis(this WebApplicationBuilder builder, string redisStringConnection)
    {
        builder.Services.AddStackExchangeRedisCache(options =>
        {
            options.Configuration = redisStringConnection;
            //options.InstanceName = "Basket";
        });
    }
}
