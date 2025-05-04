using Mentorile.Client.Web.Services.Abstracts;
using Mentorile.Shared.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Mentorile.Client.Web.Controllers;

[Authorize]
[Route("[controller]")]
public class UsersController : Controller
{
    private readonly IUserService _userService;
    private readonly ISharedIdentityService _sharedIdentityService;
    public UsersController(IUserService userService, ISharedIdentityService sharedIdentityService)
    {
        _userService = userService;
        _sharedIdentityService = sharedIdentityService;
    }

    [HttpGet]
    public async Task<IActionResult> Index()
        => View(await _userService.GetUser(_sharedIdentityService.GetUserId));

}