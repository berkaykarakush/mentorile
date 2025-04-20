using Mentorile.Services.Payment.API.DTOs;
using Mentorile.Shared.Common;
using Mentorile.Shared.ControllerBases;
using Microsoft.AspNetCore.Mvc;

namespace Mentorile.Services.Payment.API.Controllers;
[ApiController]
[Route("api/[controller]")]
public class PaymentsController : CustomControllerBase
{
    [HttpPost]
    public IActionResult ReceivePayment(PaymentDTO paymentDTO)
    {
        Console.WriteLine($"****************Odeme Bilgileri****************");
        Console.WriteLine($"Card Name: {paymentDTO.CardName}");
        Console.WriteLine($"Card Number: {paymentDTO.CardNumber}");
        Console.WriteLine($"Expiration: {paymentDTO.Expiration}");
        Console.WriteLine($"CVV: {paymentDTO.CVV}");
        Console.WriteLine($"Total Price: {paymentDTO.TotalPrice}");
        var result = Result<string>.Success("Odeme basarili");
        return CreateActionResultInstance(result);
    }
}