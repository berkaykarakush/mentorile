using Microsoft.EntityFrameworkCore;

namespace Mentorile.Services.Note.Infrastructure.Persistence;

public class AppDbContext : DbContext
{
    private const string DEFAULT_SCHEMA = "notes";
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }
    
    public DbSet<Domain.Entities.Note> Notes { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Domain.Entities.Note>().ToTable("Notes", DEFAULT_SCHEMA);

        // otomatik olarak tum sorgulara IsDeleted = false ekler
        modelBuilder.Entity<Domain.Entities.Note>().HasQueryFilter(n => !n.IsDeleted);

        base.OnModelCreating(modelBuilder);
    }

}