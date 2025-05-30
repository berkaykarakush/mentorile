using Mentorile.Client.Web.Models;
using Mentorile.Client.Web.Services.Abstracts;
using Mentorile.Shared.Services;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Mentorile.Client.WebControllers;


[Route("[controller]")]
public class AuthController : Controller
{
    private readonly IIdentityService _identityService;
    private readonly ISharedIdentityService _sharedIdentityService;
    public AuthController(IIdentityService identityService, ISharedIdentityService sharedIdentityService)
    {
        _identityService = identityService;
        _sharedIdentityService = sharedIdentityService;
    }

    [HttpGet("SignIn")]
    public IActionResult SignIn() => View();

    [HttpPost("SignIn")]
    public async Task<IActionResult> SignIn(SignInInput signInInput)
    {
        if (!ModelState.IsValid) return View();

        var response = await _identityService.SignIn(signInInput);
        if (!response.IsSuccess)
        {
            response.ErrorDetails.ForEach(x => { ModelState.AddModelError(String.Empty, x); });
            return View();
        }


        return RedirectToAction(nameof(Index), "Home");
    }


    [HttpGet("register")]
    public IActionResult Register() => View();

    [HttpPost("register")]
    public async Task<IActionResult> Register(RegisterInput registerInput)
    {
        var response = await _identityService.Register(registerInput);
        if (!response.IsSuccess)
        {
            response.ErrorDetails.ForEach(x => { ModelState.AddModelError(string.Empty, x); });
            return View();
        }
        // return RedirectToAction(nameof(Index), "Home");

        // Eğer kullanıcı status'ü Pending ise ConfirmEmail ekranına yönlendir

        return RedirectToAction(nameof(ConfirmEmail), "Auth");
    }

    [HttpGet("error")]
    public IActionResult Error() => View();

    [HttpGet("logout")]
    public async Task<IActionResult> Logout()
    {
        await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
        await _identityService.RevokeRefreshToken();
        return RedirectToAction(nameof(SignIn), "Auth");
    }

    // [Authorize]
    [HttpGet("confirmEmail")]
    public IActionResult ConfirmEmail() => View();

    // [Authorize]
    [HttpPost("confirmEmail")]
    public async Task<IActionResult> ConfirmEmail(ConfirmEmailInput emailInput)
    {
        emailInput.UserId = _sharedIdentityService.GetUserId;
        var response = await _identityService.ConfirmEmail(emailInput);
        if (!response.IsSuccess)
        {
            response.ErrorDetails.ForEach(x => { ModelState.AddModelError(string.Empty, x); });
            return View();
        }
        return RedirectToAction(nameof(Index), "Home");
    }

    [HttpGet("reConfirmEmail")]
    public async Task<IActionResult> ResendConfirmEmail()
    {
        var userId = _sharedIdentityService.GetUserId;
        if (string.IsNullOrEmpty(userId)) return RedirectToAction(nameof(SignIn));

        var response = await _identityService.ResendConfirmEmail(userId);
        if (!response.IsSuccess) TempData["ReConfirmError"] = string.Join(", ", response.ErrorDetails);
        else TempData["ReConfirmSuccess"] = "Doğrulama kodunuz tekrar gönderildi.";

        return RedirectToAction(nameof(ConfirmEmail));
    }

    [HttpGet("changePassword")]
    public IActionResult ChangePassword() => View();

    [HttpPost("changePassword")]
    public async Task<IActionResult> changePassword(ChangePasswordInput passwordInput)
    { 
        passwordInput.UserId = _sharedIdentityService.GetUserId;
        var response = await _identityService.ChangePasswordAsync(passwordInput);
        if (!response.IsSuccess)
        {
            response.ErrorDetails.ForEach(x => { ModelState.AddModelError(string.Empty, x); });
            return View();
        }
        return RedirectToAction(nameof(Index), "Home");
    }

}