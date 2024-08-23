using BuildingBlocs.Behaviors;
using System.Reflection;

namespace Basket.API;

public static class WebApplicationBuilderExtensions
{
    public static void ConfigureMediatR(this WebApplicationBuilder builder, Assembly assembly)
    {
        builder.Services.AddMediatR(config =>
        {
            config.RegisterServicesFromAssembly(assembly);
            config.AddOpenBehavior(typeof(Validator<,>));
            config.AddOpenBehavior(typeof(Logger<,>));
        });
    }

    public static void ConfigureRepos(this WebApplicationBuilder builder)
    {
        builder.Services.AddScoped<IBasketRepository, BasketRepository>();
        builder.Services.Decorate<IBasketRepository, CachedBasketRepository>();
    }

    public static void ConfigureMarten(this WebApplicationBuilder builder, string postgresStringConnection)
    {
        builder.Services.AddMarten(options =>
        {
            options.Connection(postgresStringConnection);
            options.Schema.For<ShoppingCart>().Identity(x => x.UserName);
        }).UseLightweightSessions();
    }

    public static void ConfigureRedis(this WebApplicationBuilder builder, string redisStringConnection)
    {
        builder.Services.AddStackExchangeRedisCache(options =>
        {
            options.Configuration = redisStringConnection;
            //options.InstanceName = "Basket";
        });
    }
}
