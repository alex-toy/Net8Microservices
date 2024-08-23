using Basket.API;
using HealthChecks.UI.Client;
using Microsoft.AspNetCore.Diagnostics.HealthChecks;

WebApplicationBuilder builder = WebApplication.CreateBuilder(args);

builder.ConfigureApplicationServices();

// ????????
//builder.Services.AddValidatorsFromAssembly(assembly);

string postgresStringConnection = builder.Configuration.GetConnectionString("Database")!;
string redisStringConnection = builder.Configuration.GetConnectionString("Redis")!;

builder.ConfigureDataServices(postgresStringConnection, redisStringConnection);

builder.ConfigureGrpc();

builder.ConfigureCrossCuttingServices(postgresStringConnection, redisStringConnection);







WebApplication app = builder.Build();

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
