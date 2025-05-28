using MediatR;
using Mentorile.IdentityServer.Commands;
using Mentorile.Shared.ControllerBases;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Mentorile.IdentityServer.Controllers;

[ApiController]
[Route("api/[controller]/[action]")]
public class AuthController : CustomControllerBase
{
    private readonly IMediator _mediator;

    public AuthController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [AllowAnonymous]
    [HttpPost]
    public async Task<IActionResult> Register(RegisterUserCommand command)
        => CreateActionResultInstance(await _mediator.Send(command));

    // TODO: Login islemi buraya tasinacak
    [HttpPost]
    public async Task<IActionResult> Login(LoginUserCommand command)
        => CreateActionResultInstance(await _mediator.Send(command));

    // TODO: Change password islemi gerceklestirilecek
    [HttpPost]
    public async Task<IActionResult> ChangePassword(ChangePasswordCommand command)
        => CreateActionResultInstance(await _mediator.Send(command));

    [HttpPost]
    public async Task<IActionResult> ConfirmEmail(ConfirmEmailCommand command)
        => CreateActionResultInstance(await _mediator.Send(command));
}