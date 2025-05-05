using Microsoft.EntityFrameworkCore;

namespace Mentorile.Services.Course.Infrastructure.Persistence;
public class AppDbContext : DbContext
{
    public const string DEFAULT_SCHEMA = "coursing";
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) {}

    public DbSet<Domain.Entities.Course> Courses { get; set; }
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Domain.Entities.Course>().ToTable("Courses", DEFAULT_SCHEMA);

        // tum sorgular otomatik olarak IsDeleted = false seklinde filtrelenecektir
        modelBuilder.Entity<Domain.Entities.Course>().HasQueryFilter(x => !x.IsDeleted);

        base.OnModelCreating(modelBuilder);
    }
}