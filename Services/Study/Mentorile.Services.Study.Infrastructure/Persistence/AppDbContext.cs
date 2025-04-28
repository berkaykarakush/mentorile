using Microsoft.EntityFrameworkCore;

namespace Mentorile.Services.Study.Infrastructure.Persistence;
public class AppDbContext : DbContext
{
    public const string DEFAULT_SCHEMA = "studying";
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options){}

    public DbSet<Domain.Entities.Study> Studies { get; set; }
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Domain.Entities.Study>().ToTable("Studies", DEFAULT_SCHEMA);
        base.OnModelCreating(modelBuilder);
    }
}