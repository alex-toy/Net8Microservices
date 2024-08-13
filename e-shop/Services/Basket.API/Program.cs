using BuildingBlocs.Behaviors;
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

builder.Services.AddMarten(options =>
{
    string connectionString = builder.Configuration.GetConnectionString("Database")!;
    options.Connection(connectionString);
    options.Schema.For<ShoppingCart>().Identity(x => x.UserName);
}).UseLightweightSessions();

builder.Services.AddScoped<IBasketRepository, BasketRepository>();



var app = builder.Build();

// Configure the HTTP request pipeline.
app.MapCarter();
app.UseExceptionHandler(options => { });

app.Run();
