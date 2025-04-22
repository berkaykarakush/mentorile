using System.Runtime.InteropServices;
using Mentorile.IdentityServer;
using Mentorile.IdentityServer.Data;
using Mentorile.IdentityServer.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Serilog;

Log.Logger = new LoggerConfiguration()
    .WriteTo.Console()
    .CreateBootstrapLogger();

Log.Information("Starting up");

try
{
    var builder = WebApplication.CreateBuilder(args);

    builder.Host.UseSerilog((ctx, lc) => lc
        .WriteTo.Console(outputTemplate: "[{Timestamp:HH:mm:ss} {Level}] {SourceContext}{NewLine}{Message:lj}{NewLine}{Exception}{NewLine}")
        .Enrich.FromLogContext()
        .ReadFrom.Configuration(ctx.Configuration));
    
    builder.WebHost.UseUrls("http://+:80");

    var app = builder
        .ConfigureServices()
        .ConfigurePipeline();

    using var scope = app.Services.CreateScope();
    var serviceProvider = scope.ServiceProvider;
    var applicationDbContext = serviceProvider.GetRequiredService<ApplicationDbContext>();
    applicationDbContext.Database.Migrate();

    var userManager = serviceProvider.GetRequiredService<UserManager<ApplicationUser>>();
    if(!userManager.Users.Any())
    {
        userManager.CreateAsync(new ApplicationUser
        {
            UserName = "admin",
            Email = "admin@mentorile.com",
            EmailConfirmed = true,
            PhoneNumber = "05554443322"
        }, "Password12*").Wait();
    }
    

    app.Run();
}
catch (Exception ex) when (ex is not HostAbortedException)
{
    Log.Fatal(ex, "Unhandled exception");
}
finally
{
    Log.Information("Shut down complete");
    Log.CloseAndFlush();
}