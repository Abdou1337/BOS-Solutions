using BOS.Application.DependencyInjection;
using BOS.Infrastructure.DependencyInjection;

var builder = WebApplication.CreateBuilder(args);

// Add services
builder.Services.AddApplicationServices();
builder.Services.AddInfrastructureServices(
    builder.Configuration.GetConnectionString("Default") ?? "Data Source=bos.db",
    useSqlite: true);

builder.Services.AddOpenApi();
builder.Services.AddSignalR();
builder.Services.AddAuthentication();
builder.Services.AddAuthorization();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseAuthentication();
app.UseAuthorization();

app.MapGet("/health", () => Results.Ok(new { Status = "Healthy", Timestamp = DateTimeOffset.UtcNow }))
    .WithName("HealthCheck");

app.Run();

/// <summary>
/// Entry point marker for integration tests.
/// </summary>
public partial class Program;
