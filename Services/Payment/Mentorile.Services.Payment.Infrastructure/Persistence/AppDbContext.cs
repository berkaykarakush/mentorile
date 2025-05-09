using Microsoft.EntityFrameworkCore;

namespace Mentorile.Services.Payment.Infrastrucutre.Persistence;
public class AppDbContext : DbContext
{
    private const string DEFAULT_SCHEMA = "paymenting";
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) {}
    public DbSet<Domain.Entities.Payment> Payments { get; set; }
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Domain.Entities.Payment>().ToTable("Payments", DEFAULT_SCHEMA);
        base.OnModelCreating(modelBuilder);
    }
}