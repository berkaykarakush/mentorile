using Microsoft.AspNetCore.Mvc;

namespace Mentorile.Client.Web.Controllers;

[Route("[controller]")]
public class DiscountsController : Controller
{
    private readonly ILogger<DiscountsController> _logger;

    public DiscountsController(ILogger<DiscountsController> logger)
    {
        _logger = logger;
    }

    public IActionResult Index()
    {
        return View();
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View("Error!");
    }
}