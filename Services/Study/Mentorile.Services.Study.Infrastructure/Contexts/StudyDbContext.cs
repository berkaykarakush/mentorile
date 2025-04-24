using Microsoft.EntityFrameworkCore;

namespace Mentorile.Services.Study.Infrastructure.Contexts;
public class StudyDbContext : DbContext
{
    public const string DEFAULT_SCHEMA = "studying";
    public StudyDbContext(DbContextOptions<StudyDbContext> options) : base(options){}

    public DbSet<Domain.Core.Study> Studies { get; set; }
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Domain.Core.Study>().ToTable("Studies", DEFAULT_SCHEMA);
        base.OnModelCreating(modelBuilder);
    }
}