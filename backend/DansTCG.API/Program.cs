using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.EntityFrameworkCore;
using DansTCG.Infrastructure.Data;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// CORS for frontend (configure via FRONTEND_URLS=comma-separated or FRONTEND_URL single; defaults include local)
var frontendUrlsSetting = builder.Configuration["FRONTEND_URLS"] ?? Environment.GetEnvironmentVariable("FRONTEND_URLS");
var frontendUrl = builder.Configuration["FRONTEND_URL"] ?? Environment.GetEnvironmentVariable("FRONTEND_URL");
var allowedOrigins = new List<string> { "http://localhost:5173", "http://localhost:3000" };
if (!string.IsNullOrWhiteSpace(frontendUrlsSetting))
{
    var urls = frontendUrlsSetting.Split(',', StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries);
    allowedOrigins.AddRange(urls);
}
else if (!string.IsNullOrWhiteSpace(frontendUrl))
{
    allowedOrigins.Add(frontendUrl);
}
builder.Services.AddCors(options =>
{
    options.AddPolicy("Frontend", policy =>
        policy.WithOrigins(allowedOrigins.ToArray())
              .AllowAnyHeader()
              .AllowAnyMethod());
});

// Auth (Microsoft Entra ID) - expects AZURE_AD_TENANT_ID and AZURE_AD_CLIENT_ID
var tenantId = builder.Configuration["AZURE_AD_TENANT_ID"] ?? Environment.GetEnvironmentVariable("AZURE_AD_TENANT_ID");
var clientId = builder.Configuration["AZURE_AD_CLIENT_ID"] ?? Environment.GetEnvironmentVariable("AZURE_AD_CLIENT_ID");
if (!string.IsNullOrWhiteSpace(tenantId) && !string.IsNullOrWhiteSpace(clientId))
{
    var authority = $"https://login.microsoftonline.com/{tenantId}/v2.0";
    builder.Services
        .AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
        .AddJwtBearer(options =>
        {
            options.Authority = authority;
            options.TokenValidationParameters = new TokenValidationParameters
            {
                ValidateIssuer = true,
                // Accept either bare clientId or api://{clientId} depending on app registration config
                ValidAudiences = new[] { clientId, $"api://{clientId}" }
            };
        });
}
else
{
    Console.WriteLine("[warn] AZURE_AD_TENANT_ID or AZURE_AD_CLIENT_ID not set. Authentication is not configured.");
}

// Database
var pgConn = builder.Configuration["POSTGRES_CONNECTION_STRING"] ??
             Environment.GetEnvironmentVariable("POSTGRES_CONNECTION_STRING");
if (!string.IsNullOrWhiteSpace(pgConn))
{
    builder.Services.AddDbContext<AppDbContext>(options => options.UseNpgsql(pgConn));
}
else
{
    Console.WriteLine("[warn] POSTGRES_CONNECTION_STRING is not set. DbContext will not be configured.");
}

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseCors("Frontend");
app.UseAuthentication();
app.UseAuthorization();
app.MapControllers();

// Lightweight DB probe endpoint
app.MapGet("/health/db", async (AppDbContext? db) =>
{
    if (db is null) return Results.Problem("DbContext not configured");
    try
    {
        var canConnect = await db.Database.CanConnectAsync();
        return canConnect ? Results.Ok(new { db = "ok" }) : Results.Problem("DB unreachable");
    }
    catch (Exception ex)
    {
        return Results.Problem(ex.Message);
    }
});

// Attempt to ensure database exists (dev bootstrap)
using (var scope = app.Services.CreateScope())
{
    var db = scope.ServiceProvider.GetService<AppDbContext>();
    if (db != null)
    {
        try
        {
            // Dev bootstrap: ensure DB exists in local scenarios
            db.Database.EnsureCreated();

            // Optional: apply EF Core migrations automatically on startup when enabled.
            // Gate this with an environment variable to avoid unintended schema changes.
            var applyMigrations = Environment.GetEnvironmentVariable("APPLY_MIGRATIONS_ON_STARTUP");
            if (!string.IsNullOrWhiteSpace(applyMigrations) && applyMigrations.Equals("true", StringComparison.OrdinalIgnoreCase))
            {
                Console.WriteLine("[info] APPLY_MIGRATIONS_ON_STARTUP=true detected. Applying EF Core migrations...");
                db.Database.Migrate();
                Console.WriteLine("[info] EF Core migrations applied successfully.");
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"[warn] Database bootstrap/migration skipped or failed: {ex.Message}");
        }
    }
}
app.Run();
