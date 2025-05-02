using MassTransit;
using MassTransit.Courier;
using Mentorile.IdentityServer.Models;
using Mentorile.Shared.Common;
using Mentorile.Shared.Messages.Events;
using Mentorile.Shared.Services.Abstracts;
using Microsoft.AspNetCore.Identity;

namespace Mentorile.IdentityServer.Services;
public class AuthService : IAuthService
{
    private readonly IExecutor _executor;
    private readonly UserManager<ApplicationUser> _userManager;
    private readonly SignInManager<ApplicationUser> _signInManager;
    private readonly IPublishEndpoint _publishEndpoint;

    public AuthService(UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager, IPublishEndpoint publishEndpoint, IExecutor executor)
    {
        _userManager = userManager;
        _signInManager = signInManager;
        _publishEndpoint = publishEndpoint;
        _executor = executor;
    }

    public async Task<Result<Guid>> AuthenticateAsync(string identifier, string password)
    => await _executor.ExecuteAsync(async () => {
        var user = await _userManager.FindByEmailAsync(identifier) ?? await _userManager.FindByNameAsync(identifier);;
        if (user == null) throw new Exception("User not found.");

        var result = await _signInManager.CheckPasswordSignInAsync(user, password, lockoutOnFailure: false);
        if(!result.Succeeded) throw new Exception("Invalid credentials.");

        return user.Id;
    });

    public async Task<Result<bool>> ChangePasswordAsync(Guid userId, string currentPassword, string newPassword)
        => await _executor.ExecuteAsync(async () =>
        {
            var user = await _userManager.FindByIdAsync(userId.ToString());
            if (user == null) throw new Exception("User not found.");

            var result = await _userManager.ChangePasswordAsync(user, currentPassword, newPassword);
            if (!result.Succeeded)
                throw new Exception(string.Join(",", result.Errors.Select(e => e.Description)));

            return true; 
        });

    public async Task<Result<Guid>> RegisterAsync(string name, string surname, string email, string phoneNumber, string password)
    => await _executor.ExecuteAsync(async () => {
        name = name.Trim().ToLower();
        surname = surname.Trim().ToLower();
        email = email.Trim().ToLower();

        var user = new ApplicationUser(){
            UserName = email,
            Name = name,
            Surname = surname,
            Email = email,
            PhoneNumber = phoneNumber
        };
        
        var result = await _userManager.CreateAsync(user, password);
        if(!result.Succeeded) throw new Exception(string.Join(",", result.Errors.Select(e => e.Description))); 
        
        var userId = user.Id;
        await _publishEndpoint.Publish<UserRegisteredEvent>(new UserRegisteredEvent(){
            UserId = userId,
            Name = name,
            Surname = surname,
            Email = email,
            PhoneNumber = phoneNumber,
            CreateAt = DateTime.UtcNow
        });
        return userId;
    });
}