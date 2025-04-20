using Mentorile.Client.Web.Services.Abstracts;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Mentorile.Client.Web.Controllers;
[Authorize]
[Route("[controller]")]
public class UsersController : Controller
{
   private readonly IUserService _userService;

    public UsersController(IUserService userService)
    {
        _userService = userService;
    }

    [HttpGet]
    public async Task<IActionResult> Index()
    {
        return View(await _userService.GetUser());
    }
}