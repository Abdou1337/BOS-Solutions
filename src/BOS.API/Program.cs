var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();

app.MapGet("/", () => "BOS Solutions API");

app.Run();
