using MediatR;
using Mentorile.Services.User.Application.Commands;
using Mentorile.Services.User.Application.Queries;
using Mentorile.Shared.ControllerBases;
using Microsoft.AspNetCore.Mvc;

namespace Mentorile.Services.User.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class UsersController : CustomControllerBase
{
    private readonly IMediator _mediator;

    public UsersController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpDelete("hard")]
    public async Task<IActionResult> HardDelete(HardDeleteUserCommand command)
        => CreateActionResultInstance(await _mediator.Send(command));

    [HttpDelete("soft")]
    public async Task<IActionResult> SoftDelete(SoftDeleteUserCommand command)
        => CreateActionResultInstance(await _mediator.Send(command));
    
    [HttpPut]
    public async Task<IActionResult> UpdateProfile(UpdateUserProfileCommand command)
        => CreateActionResultInstance(await _mediator.Send(command));

    [HttpGet("exists/email")]
    public async Task<IActionResult> EmailExists(CheckEmailExistsQuery query)
        => CreateActionResultInstance(await _mediator.Send(query));


    [HttpGet("exists/phone-number")]
    public async Task<IActionResult> PhoneNumberExists(CheckPhoneNumberExistsQuery query)
        => CreateActionResultInstance(await _mediator.Send(query));
    
    [HttpGet("active")]
    public async Task<IActionResult> GetAllActive(GetAllActiveUersQuery query)
        => CreateActionResultInstance(await _mediator.Send(query));

    [HttpGet]
    public async Task<IActionResult> GetAll(GetAllUsersQuery query)
        => CreateActionResultInstance(await _mediator.Send(query));
    
    [HttpGet("by-email")]
    public async Task<IActionResult> GetByEmail(GetUserByEmailQuery query)
        => CreateActionResultInstance(await _mediator.Send(query));

    [HttpGet("by-id")]
    public async Task<IActionResult> GetById(GetUserByIdQuery query)
        => CreateActionResultInstance(await _mediator.Send(query));

    [HttpGet("by-phone-number")]
    public async Task<IActionResult> GetByPhoneNumber(GetUserByPhoneNumberQuery query)
        => CreateActionResultInstance(await _mediator.Send(query));
    
    [HttpGet("search")]
    public async Task<IActionResult> Search(SearchUsersQuery query)
        => CreateActionResultInstance(await _mediator.Send(query));
}