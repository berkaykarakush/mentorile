using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;

namespace Mentorile.Services.Order.Infrastructure.Contexts;
public class DesignTimeDbContextFactory : IDesignTimeDbContextFactory<OrderDbContext>
{
    // ASPNETCORE_ENVIRONMENT=Development dotnet ef migrations add InitialCreate -s ../Mentorile.Services.Order.API/Mentorile.Services.Order.API.csproj
    public OrderDbContext CreateDbContext(string[] args)
    {
        var environment = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT") ?? "Production";

        var basePath = Path.GetFullPath(Path.Combine(Directory.GetCurrentDirectory(), "../Mentorile.Services.Order.API"));

        IConfigurationRoot configuration = new ConfigurationBuilder()
            .SetBasePath(basePath)
            .AddJsonFile("appsettings.json", optional: false)
            .AddJsonFile($"appsettings.{environment}.json", optional: true)
            .Build();

        var optionsBuilder = new DbContextOptionsBuilder<OrderDbContext>();
        var connectionString = configuration.GetConnectionString("DefaultConnection");

        optionsBuilder.UseNpgsql(connectionString,
            x => x.MigrationsAssembly(typeof(OrderDbContext).Assembly.FullName));

        return new OrderDbContext(optionsBuilder.Options);
    }
}