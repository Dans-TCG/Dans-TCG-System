using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace DansTCG.Infrastructure.Data;

public class DesignTimeDbContextFactory : IDesignTimeDbContextFactory<AppDbContext>
{
    public AppDbContext CreateDbContext(string[] args)
    {
        var optionsBuilder = new DbContextOptionsBuilder<AppDbContext>();
        // Fallback to a placeholder local connection if not provided
        var conn = Environment.GetEnvironmentVariable("POSTGRES_CONNECTION_STRING")
                   ?? "Host=localhost;Port=5432;Username=postgres;Password=postgres;Database=dans_tcg";
        optionsBuilder.UseNpgsql(conn);
        return new AppDbContext(optionsBuilder.Options);
    }
}
