using Mentorile.Shared.Common;
using Microsoft.AspNetCore.Mvc;

namespace Mentorile.Shared.ControllerBases;

[ApiController]
[Route("[controller]")]
public class CustomControllerBase : ControllerBase
{
    public IActionResult CreateActionResultInstance<T>(Result<T> result)
        => new ObjectResult(result){ StatusCode = result.StatusCode};
}