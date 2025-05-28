using Mentorile.IdentityServer;
using Mentorile.IdentityServer.Data;
using Mentorile.IdentityServer.Settings;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
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
    
    
    builder.Services.Configure<Settings>(builder.Configuration.GetSection("Settings"));
    builder.Services.AddSingleton<ISettings>(sp => sp.GetRequiredService<IOptions<Settings>>().Value);

    builder.WebHost.UseUrls("http://+:80");

    var app = builder
        .ConfigureServices()
        .ConfigurePipeline();

    using var scope = app.Services.CreateScope();
    var serviceProvider = scope.ServiceProvider;
    var applicationDbContext = serviceProvider.GetRequiredService<ApplicationDbContext>();
    applicationDbContext.Database.Migrate();
    
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