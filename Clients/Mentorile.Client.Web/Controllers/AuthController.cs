using Mentorile.Client.Web.Models;
using Mentorile.Client.Web.Services.Abstracts;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;

namespace Mentorile.Client.Web.Controllers;
[Route("auth")]
public class AuthController : Controller
{
    private readonly IIdentityService _identityService;

    public AuthController(IIdentityService identityService)
    {
        _identityService = identityService;
    }

    [HttpGet("signin")]
    public IActionResult SignIn() => View();

    [HttpPost("signin")]
    public async Task<IActionResult> SignIn(SignInInput signInInput)
    {
        if(!ModelState.IsValid) return View();

        var response = await _identityService.SignIn(signInInput);
        if(!response.IsSuccess){
            response.ErrorDetails.ForEach(x => {  ModelState.AddModelError(String.Empty, x); });
            return View();
        }


        return RedirectToAction(nameof(Index), "Home");
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
}