using AspNetCoreRateLimit;
using DotNetEnv;
using Microsoft.EntityFrameworkCore;
using TownSquareAPI.Data;
using TownSquareAPI.Services;

var builder = WebApplication.CreateBuilder(args);

Env.Load();

// add appsettings, dev appsettings, env variables (set config)
//builder.Configuration.AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
//    .AddJsonFile($"appsettings.Development.json", optional: true, reloadOnChange: true)
//    .AddEnvironmentVariables();

var connectionString = Environment.GetEnvironmentVariable("DB_CONNECTION") ??
                       $"Host={Environment.GetEnvironmentVariable("DB_HOST")};" +
                       $"Database={Environment.GetEnvironmentVariable("DB_DATABASE")};" +
                       $"Username={Environment.GetEnvironmentVariable("DB_USERNAME")};" +
                       $"Password={Environment.GetEnvironmentVariable("DB_PASSWORD")};" +
                       "SSL Mode=Require;Trust Server Certificate=true";

//if (string.IsNullOrEmpty(connectionString) || builder.Environment.IsDevelopment())
//{
//    connectionString = builder.Configuration.GetConnectionString("DefaultConnection")
//                       ?? throw new InvalidOperationException("Database connection string is not configured.");
//}

// Register DbContext with PostgreSQL
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseNpgsql(connectionString));

// Register application services
builder.Services.AddScoped<UserService>();
builder.Services.AddScoped<PostService>();
builder.Services.AddScoped<CommunityService>();
builder.Services.AddScoped<HelpPostService>();
builder.Services.AddScoped<PinService>();

builder.Services.AddMemoryCache();
builder.Services.Configure<IpRateLimitOptions>(builder.Configuration.GetSection("IpRateLimiting"));
builder.Services.AddSingleton<IRateLimitConfiguration, RateLimitConfiguration>();
builder.Services.AddInMemoryRateLimiting();

// Add AutoMapper
builder.Services.AddAutoMapper(typeof(Program));

// Add controllers and Swagger for API documentation
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.WebHost.UseUrls("http://0.0.0.0:80");

var app = builder.Build();

// Enable Swagger UI in development mode
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseAuthorization();
app.MapControllers();
app.UseIpRateLimiting();

app.Run();
