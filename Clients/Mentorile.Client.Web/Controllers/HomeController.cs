using Microsoft.AspNetCore.Mvc;

namespace Mentorile.Client.WebControllers;

[Route("[controller]")]
public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;

    public HomeController(ILogger<HomeController> logger)
    {
        _logger = logger;
    }
    
    [HttpGet("index")]
    public IActionResult Index()
    {
        return View();
    }
}
