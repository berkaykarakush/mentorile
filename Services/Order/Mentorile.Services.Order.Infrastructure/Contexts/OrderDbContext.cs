using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Internal;

namespace Mentorile.Services.Order.Infrastructure.Contexts;
public class OrderDbContext : DbContext
{
    public const string DEFAULT_SCHEMA = "ordering";
    public OrderDbContext(DbContextOptions<OrderDbContext> options) : base(options)
    {
        
    }
    public DbSet<Domain.OrderAggreagate.Order> Orders { get; set; }
    public DbSet<Domain.OrderAggreagate.OrderItem> OrderItems { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Domain.OrderAggreagate.Order>().ToTable("Orders", DEFAULT_SCHEMA);
        modelBuilder.Entity<Domain.OrderAggreagate.OrderItem>().ToTable("OrderItems", DEFAULT_SCHEMA);
        modelBuilder.Entity<Domain.OrderAggreagate.OrderItem>().Property(x => x.Price).HasColumnType("decimal(18,2)");
        modelBuilder.Entity<Domain.OrderAggreagate.Order>().OwnsOne(x => x.Address).WithOwner();
        base.OnModelCreating(modelBuilder);
    }
}