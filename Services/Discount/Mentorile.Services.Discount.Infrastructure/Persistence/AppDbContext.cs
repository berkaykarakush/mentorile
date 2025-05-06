using Mentorile.Services.Discount.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Mentorile.Services.Discount.Infrastructure.Persistence;
public class AppDbContext : DbContext
{
    private const string DEFAULT_SCHEMA = "discounting";
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) {}

    public DbSet<Domain.Entities.Discount> Discounts { get; set; }
    public DbSet<DiscountUser> DiscountUsers { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Domain.Entities.Discount>().ToTable("Discounts", DEFAULT_SCHEMA);
        modelBuilder.Entity<DiscountUser>()
            .ToTable("DiscountUsers", DEFAULT_SCHEMA)
            .HasKey(du => new { du.DiscountId, du.UserId});
        
        modelBuilder.Entity<DiscountUser>()
            .HasOne(du => du.Discount)
            .WithMany(d => d.DiscountUsers)
            .HasForeignKey(du => du.DiscountId);

        // otomatik olarak tum sorgulara IsDeleted = false ekler
        modelBuilder.Entity<Domain.Entities.Discount>().HasQueryFilter(d => !d.IsDeleted);
        modelBuilder.Entity<DiscountUser>().HasQueryFilter(du => !du.Discount.IsDeleted);

        base.OnModelCreating(modelBuilder);
    }
}