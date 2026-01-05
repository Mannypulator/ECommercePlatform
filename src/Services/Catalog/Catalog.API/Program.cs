using BuildingBlocks.Extensions;
using Carter;
using Catalog.API.Repository;
using Scalar.AspNetCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// 1. Add Service Defaults (Aspire "Glue")
// This ensures OpenTelemetry, HealthChecks, etc.
builder.AddServiceDefaults();


// 2. Add Infrastructure Services (Database)
// "catalogdb" must match the name you gave in AppHost
builder.AddNpgsqlDbContext<CatalogDbContext>("catalogdb", settings =>
{
    // Ensure the DB is created automatically locally (Development only)
    // For Production, you would use migration scripts
});

// 3. Add Application Services
// This registers MediatR, FluentValidation, Behaviors from your BuildingBlocks
builder.Services.AddBuildingBlocks(typeof(Program).Assembly);

// 4. Add Minimal API routing (Carter)
builder.Services.AddCarter();

var app = builder.Build();

// 5. Configure Pipeline
app.MapDefaultEndpoints(); // Aspire health checks
app.MapCarter();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    app.MapScalarApiReference();
    app.UseSwagger();
    app.UseSwaggerUI();

    using var scope = app.Services.CreateScope();
    var context = scope.ServiceProvider.GetRequiredService<CatalogDbContext>();
    // This will create tables if they don't exist
    context.Database.EnsureCreated();
}

app.UseHttpsRedirection();

var summaries = new[]
{
    "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
};

app.MapGet("/weatherforecast", () =>
{
    var forecast = Enumerable.Range(1, 5).Select(index =>
        new WeatherForecast
        (
            DateOnly.FromDateTime(DateTime.Now.AddDays(index)),
            Random.Shared.Next(-20, 55),
            summaries[Random.Shared.Next(summaries.Length)]
        ))
        .ToArray();
    return forecast;
})
.WithName("GetWeatherForecast");

app.Run();

record WeatherForecast(DateOnly Date, int TemperatureC, string? Summary)
{
    public int TemperatureF => 32 + (int)(TemperatureC / 0.5556);
}
