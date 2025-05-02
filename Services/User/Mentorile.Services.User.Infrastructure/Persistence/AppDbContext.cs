using Mentorile.Services.User.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Mentorile.Services.User.Infrastructure.Persistence;
public class AppDbContext : DbContext
{
    public const string DEFAULT_SCHEMA = "profilies";
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options){}

    public DbSet<UserProfile> Profiles { get; set; }
    public DbSet<Role> Roles { get; set; }
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<UserProfile>().ToTable("Profiles", DEFAULT_SCHEMA);
        modelBuilder.Entity<Role>().ToTable("Roles", DEFAULT_SCHEMA);

        // tum sorgular otomatik olarak IsDeleted = false seklinde filtrenecektir
        modelBuilder.Entity<UserProfile>().HasQueryFilter(u => !u.IsDeleted);

        base.OnModelCreating(modelBuilder);
    }
}