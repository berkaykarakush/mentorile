using MediatR;
using Mentorile.Services.Email.Application.Commands;
using Mentorile.Shared.ControllerBases;
using Microsoft.AspNetCore.Mvc;

namespace Mentorile.Services.Email.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class EmailController : CustomControllerBase
{
    private readonly IMediator _mediator;

    public EmailController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpPost]
    public async Task<IActionResult> Send(SendManualEmailCommand command)
        => CreateActionResultInstance(await _mediator.Send(command));

    private IActionResult CreateActionResultInstance(object value)
    {
        throw new NotImplementedException();
    }
}