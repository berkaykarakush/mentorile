using Microsoft.EntityFrameworkCore;

namespace Mentorile.Services.PhotoStock.Infrastructure.Persistence;
public class AppDbContext : DbContext
{
    private const string DEFAULT_SCHEMA = "photos";
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }   

    public DbSet<Domain.Entities.Photo> Photos { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Domain.Entities.Photo>().ToTable("Photos", DEFAULT_SCHEMA);
        base.OnModelCreating(modelBuilder);
    }
}