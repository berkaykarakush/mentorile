using Mentorile.Shared.Common;
using Mentorile.Shared.ControllerBases;
using Microsoft.AspNetCore.Mvc;

namespace Mentorile.Services.Payment.API.Controllers;
[ApiController]
[Route("api/[controller]")]
public class PaymentsController : CustomControllerBase
{
    [HttpPost]
    public IActionResult ReceivePayment()
    {
        var result = Result<string>.Success("Odeme basarili");
        return CreateActionResultInstance(result);
    }
}