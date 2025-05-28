using IdentityModel.Client;
using MassTransit;
using Mentorile.IdentityServer.DTOs;
using Mentorile.IdentityServer.Enums;
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
    private const string host = "http://identityserver.api";

    public AuthService(IExecutor executor, UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager, IPublishEndpoint publishEndpoint)
    {
        _executor = executor;
        _userManager = userManager;
        _signInManager = signInManager;
        _publishEndpoint = publishEndpoint;
    }

    public async Task<Result<Guid>> AuthenticateAsync(string identifier, string password)
    => await _executor.ExecuteAsync(async () =>
    {
        var user = await _userManager.FindByEmailAsync(identifier) ?? await _userManager.FindByNameAsync(identifier); ;
        if (user == null) throw new Exception("User not found.");

        var result = await _signInManager.CheckPasswordSignInAsync(user, password, lockoutOnFailure: false);
        if (!result.Succeeded) throw new Exception("Invalid credentials.");

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

    public async Task<Result<UserAuthenticatedDto>> RegisterAsync(string name, string surname, string email, string phoneNumber, string password)
    => await _executor.ExecuteAsync(async () =>
    {
        name = name.Trim().ToLower();
        surname = surname.Trim().ToLower();
        email = email.Trim().ToLower();

        // Kullanıcı oluştur
        var user = new ApplicationUser()
        {
            UserName = email,
            Name = name,
            Surname = surname,
            Email = email,
            PhoneNumber = phoneNumber,
            Status = UserStatus.Pending
        };

        var result = await _userManager.CreateAsync(user, password);
        if (!result.Succeeded) throw new Exception("Kullanıcı oluşturulamadı.");

        // Confirmation code oluştur ve kaydet (1 saat geçerli)
        var confirmationCode = new Random().Next(100000, 999999).ToString();
        var expireAt = DateTime.UtcNow.AddMinutes(10);

        var tokenPayload = $"{confirmationCode}|{expireAt:O}";
        await _userManager.SetAuthenticationTokenAsync(user, "email", "confirmation_code", tokenPayload);

        // Kullanıcıyı güncelle
        await _userManager.UpdateAsync(user);

        // Kullanıcı otomatik login olabilir
        await _signInManager.SignInAsync(user, isPersistent: false);

        // Mail gönderimi için event publish et (RabbitMQ veya başka mekanizma)
        await _publishEndpoint.Publish<UserRegisteredEvent>(new UserRegisteredEvent()
        {
            UserId = user.Id,
            Name = name,
            Surname = surname,
            Email = email,
            PhoneNumber = phoneNumber,
            ConfirmationCode = confirmationCode,
            EmailConfirmationCodeExpireAt = expireAt,
            CreateAt = DateTime.UtcNow
        });

        // ✅ Kayıttan sonra token almak için arkaplanda connect/token endpoint'ine istek at
        var tokenResponse = await RequestTokenAsync(email, password);

        return new UserAuthenticatedDto
        {
            UserId = user.Id.ToString(),
            AccessToken = tokenResponse.AccessToken,
            RefreshToken = tokenResponse.RefreshToken,
            ExpiresIn = tokenResponse.ExpiresIn,
            TokenType = tokenResponse.TokenType
        };
    });

    public async Task<Result<bool>> ConfirmEmailAsync(string userId, string confirmationCode)
        => await _executor.ExecuteAsync(async () =>
        {
            var user = await _userManager.FindByIdAsync(userId);
            if (user == null) throw new Exception("User not found.");

            if (user.Status == UserStatus.Active) throw new Exception("Email is already confirmed.");

            // UserManager'dan confirmation code kontrolü
            var tokenPayload = await _userManager.GetAuthenticationTokenAsync(user, "email", "confirmation_code");
            if (string.IsNullOrEmpty(tokenPayload)) throw new Exception("Confirmation code not found.");

            var parts = tokenPayload.Split('|');
            if(parts.Length != 2) throw new Exception("Confirmation code format invalid.");


            var code = parts[0];
            var expireAt = DateTime.Parse(parts[1]);
            if(code != confirmationCode) throw new Exception("Confirmation code failed.");
            if(DateTime.UtcNow > expireAt) throw new Exception("Confirmation code date invalid.");

            // kullanciyi aktif yap
            user.Active();
            await _userManager.UpdateAsync(user);

            // kod gecerliligini kaldir
            await _userManager.RemoveAuthenticationTokenAsync(user, "email", "confirmation_code");

            return true;
        });

    private async Task<TokenResponse> RequestTokenAsync(string username, string password)
    {
        var client = new HttpClient();
        // var disco = await client.GetDiscoveryDocumentAsync(host);
        var disco = await client.GetDiscoveryDocumentAsync(new DiscoveryDocumentRequest
        {
            Address = host,
            Policy = new DiscoveryPolicy
            {
                RequireHttps = false
            }
        });

        if (disco.IsError) throw new Exception($"Discovery document error: {disco.Error}");

        var tokenRequest = new PasswordTokenRequest()
        {
            Address = disco.TokenEndpoint,
            ClientId = "MentorileWebMVCClientForUser",
            ClientSecret = "secret",
            UserName = username,
            Password = password
        };

        var tokenResponse = await client.RequestPasswordTokenAsync(tokenRequest);

        if(tokenResponse.IsError) throw new Exception($"Token error: {tokenResponse.Error}");
        return tokenResponse;
    }
}