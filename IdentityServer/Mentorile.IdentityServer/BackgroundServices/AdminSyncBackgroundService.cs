
using MassTransit;
using Mentorile.IdentityServer.Models;
using Mentorile.Shared.Messages.Events;
using Microsoft.AspNetCore.Identity;
using Microsoft.OpenApi.Writers;

namespace Mentorile.IdentityServer.BackgroundServices;
public class AdminSyncBackgroundService : BackgroundService
{
    private readonly IServiceProvider _serviceProvider;
    private readonly ILogger<AdminSyncBackgroundService> _logger;

    public AdminSyncBackgroundService(IServiceProvider serviceProvider, ILogger<AdminSyncBackgroundService> logger)
    {
        _serviceProvider = serviceProvider;
        _logger = logger;
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        // 5 s bekle
        await Task.Delay(5000, stoppingToken);

        using var scope = _serviceProvider.CreateScope();
        var _userManager = scope.ServiceProvider.GetRequiredService<UserManager<ApplicationUser>>();
        var _publishEndpoint = scope.ServiceProvider.GetRequiredService<IPublishEndpoint>(); // <-- Scoped olarak alındı

        if(!_userManager.Users.Any())
        {
            var adminUser =  new ApplicationUser
            {
                Name  = "Admin",
                Surname = "Mentorile.com",
                UserName = "admin",
                Email = "admin@mentorile.com",
                EmailConfirmed = true,
                PhoneNumber = "05554443322"
            };

            var result = await _userManager.CreateAsync(adminUser, "Password12*");
            if(!result.Succeeded)
            {
                _logger.LogError("Failed to create admin user: {Errors}", string.Join(", ", result.Errors.Select(e => e.Description)));
                return;
            }

            _logger.LogInformation("Admin user created and event published successfully.");
            await _publishEndpoint.Publish<UserRegisteredEvent>(new UserRegisteredEvent()
            {
                UserId = adminUser.Id,
                Name = adminUser.Name,
                Surname = adminUser.Surname,
                Email = adminUser.Email,
                PhoneNumber = adminUser.PhoneNumber,
                CreateAt = DateTime.UtcNow
            }, stoppingToken);
        }
    }
}