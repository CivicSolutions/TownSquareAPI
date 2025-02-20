using Microsoft.EntityFrameworkCore;
using TownSquareAPI.Data;
using TownSquareAPI.Services;
using DotNetEnv;

var builder = WebApplication.CreateBuilder(args);

Env.Load();

// Build the connection string dynamically or fall back to appsettings.json
var connectionString = Environment.GetEnvironmentVariable("DB_CONNECTION") ??
                       $"Host={Environment.GetEnvironmentVariable("DB_HOST")};" +
                       $"Port={Environment.GetEnvironmentVariable("DB_PORT")};" +
                       $"Database={Environment.GetEnvironmentVariable("DB_DATABASE")};" +
                       $"Username={Environment.GetEnvironmentVariable("DB_USERNAME")};" +
                       $"Password={Environment.GetEnvironmentVariable("DB_PASSWORD")};";

if (string.IsNullOrEmpty(connectionString))
{
    connectionString = builder.Configuration.GetConnectionString("DefaultConnection")
                       ?? throw new InvalidOperationException("Database connection string is not configured.");
}

// Register DbContext with PostgreSQL
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseNpgsql(connectionString));

// Register application services
builder.Services.AddScoped<UserService>();
builder.Services.AddScoped<PostService>();
builder.Services.AddScoped<CommunityService>();
builder.Services.AddScoped<HelpPostService>();
builder.Services.AddScoped<PinService>();

// Add controllers and Swagger for API documentation
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Enable Swagger UI in development mode
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseSwagger();
app.UseSwaggerUI();
app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();

app.Run();