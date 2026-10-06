using Microsoft.EntityFrameworkCore;
using Npgsql;
using Server.Models;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

const string ANY_ORIGINE_POLICY = "AnyOrigin";

builder.Services.AddCors(options =>
{
    options.AddPolicy(ANY_ORIGINE_POLICY, policy =>
    {
        policy.AllowAnyOrigin()
            .AllowAnyMethod()
            .AllowAnyHeader();
    });
});

builder.Services.AddControllers();
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();

builder.Services.AddDbContext<AerDbContext>(options =>
{
    var connectionString = new NpgsqlConnectionStringBuilder
    {
        Host = GetRequiredSetting("DB_HOST"),
        Port = int.Parse(GetRequiredSetting("DB_PORT")),
        Database = GetRequiredSetting("DB_NAME"),
        Username = GetRequiredSetting("DB_USER"),
        Password = GetRequiredSetting("DB_PASSWORD")
    }.ConnectionString;

    options.UseNpgsql(connectionString);
});

var app = builder.Build();

app.UseDefaultFiles();
app.MapStaticAssets();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();

    app.UseCors(ANY_ORIGINE_POLICY);
}

app.UseAuthorization();

app.MapControllers();
app.Map("api/{**slug}", HandleApiFallback);
app.MapFallbackToFile("/index.html");

app.Run();

string GetRequiredSetting(string key) =>
    builder.Configuration[key]
    ?? throw new InvalidOperationException($"Missing required configuration value '{key}'.");

Task HandleApiFallback(HttpContext context)
{
    context.Response.StatusCode = StatusCodes.Status404NotFound;
    return Task.CompletedTask;
}