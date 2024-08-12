using BuildingBlocs.Behaviors;
using System.Reflection;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container
Assembly assembly = typeof(Program).Assembly;

builder.Services.AddMediatR(config =>
{
    config.RegisterServicesFromAssembly(assembly);
    config.AddOpenBehavior(typeof(Validator<,>));
});

builder.Services.AddValidatorsFromAssembly(assembly);

builder.Services.AddCarter();

builder.Services.AddMarten(options =>
{
    string connectionString = builder.Configuration.GetConnectionString("Database")!;
    options.Connection(connectionString);
})
.UseLightweightSessions();

var app = builder.Build();

// Configure the HTTP request pipeline
app.MapCarter();

app.Run();
