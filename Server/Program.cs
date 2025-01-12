using Server.Model;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

const string ANY_ORIGINE_POLICY = "AnyOrigin";

builder.Services.AddCors(options =>
{
    options.AddPolicy(ANY_ORIGINE_POLICY, policy =>
    {
        policy.AllowAnyOrigin();
    });
});

builder.Services.AddControllers();
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();

builder.Services.AddDbContext<AerDbContext>();

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

app.MapFallbackToFile("/index.html");

app.Run();
