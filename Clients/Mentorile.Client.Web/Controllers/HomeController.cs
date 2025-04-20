using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Mentorile.Client.Web.Models;
using Microsoft.AspNetCore.Diagnostics;
using Mentorile.Client.Web.Exceptions;

namespace Mentorile.Client.Web.Controllers;

public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;

    public HomeController(ILogger<HomeController> logger)
    {
        _logger = logger;
    }

    public IActionResult Index()
    {
        return View();
    }

    public IActionResult Privacy()
    {
        return View();
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        var errorFeature = HttpContext.Features.Get<IExceptionHandlerFeature>();
        if(errorFeature != null && errorFeature.Error is UnAuthorizeException) 
            return RedirectToAction(nameof(AuthController.Logout), "Auth");

        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}
