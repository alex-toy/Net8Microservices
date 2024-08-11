var builder = WebApplication.CreateBuilder(args);

// Add services to the container
builder.Services.AddCarter();

builder.Services.AddMediatR(config =>
{
    config.RegisterServicesFromAssembly(typeof(Program).Assembly);
});

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
