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

//builder.Services.AddMarten(opts =>
//{
//    opts.Connection(builder.Configuration.GetConnectionString("Database")!);
//    opts.Schema.For<ShoppingCart>().Identity(x => x.UserName);
//}).UseLightweightSessions();


var app = builder.Build();


// Configure the HTTP request pipeline.
app.MapCarter();
app.UseExceptionHandler(options => { });

app.Run();
