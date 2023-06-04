using Microsoft.EntityFrameworkCore;

using StixVuln.Core.Vulnerability;

namespace StixVuln.Infrastructure.Data;

public class AppDbContext : DbContext
{
    public AppDbContext(
        DbContextOptions<AppDbContext> options) : base(options)
    {
    }

    public DbSet<Vulnerability> Vulnerabilities { get; set; } = null!;

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder
            .ApplyConfigurationsFromAssembly(
                typeof(AppDbContext).Assembly);
        base.OnModelCreating(modelBuilder);
    }
}
