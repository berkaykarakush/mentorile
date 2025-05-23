using Mentorile.Services.Email.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Mentorile.Services.Email.Infrastructure.Persistence;

public class AppDbContext : DbContext
{
    private const string DEFAULT_SCHEMA = "emails";
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

    public DbSet<EmailMessage> EmailMessages { get; set; }
    public DbSet<EmailLog> EmailLogs { get; set; }
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<EmailMessage>().ToTable("EmailMessages", DEFAULT_SCHEMA);
        modelBuilder.Entity<EmailLog>().ToTable("EmailLogs", DEFAULT_SCHEMA);

        base.OnModelCreating(modelBuilder);
    }
}