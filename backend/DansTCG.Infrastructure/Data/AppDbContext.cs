using Microsoft.EntityFrameworkCore;

namespace DansTCG.Infrastructure.Data;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

    // Minimal probe entity to validate migrations/connection
    public DbSet<DbProbe> Probes => Set<DbProbe>();
}

public class DbProbe
{
    public int Id { get; set; }
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
}
