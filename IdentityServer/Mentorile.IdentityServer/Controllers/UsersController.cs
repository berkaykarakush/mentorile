using System.IdentityModel.Tokens.Jwt;
using Mentorile.IdentityServer.Handlers;
using Mentorile.IdentityServer.Models;
using Mentorile.Shared.ControllerBases;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Mentorile.IdentityServer.Controllers;

[ApiController]
[Route("api/[controller]/[action]")]
public class UsersController : CustomControllerBase
{
    private readonly UserManager<ApplicationUser> _userManager;
    private readonly IPasswordValidator<ApplicationUser> _passwordValidator;
    private readonly MediatR.IMediator _mediator;

    public UsersController(UserManager<ApplicationUser> userManager, IPasswordValidator<ApplicationUser> passwordValidator, MediatR.IMediator mediator)
    {
        _userManager = userManager;
        _passwordValidator = passwordValidator;
        _mediator = mediator;
    }

    [AllowAnonymous]
    [HttpPost]
    public async Task<IActionResult> Register(RegisterUserCommand command)
        => CreateActionResultInstance(await _mediator.Send(command));
        
    [HttpGet]
    public async Task<IActionResult> GetUser()
    {
        var userIdClaim = User.Claims.FirstOrDefault(u => u.Type == JwtRegisteredClaimNames.Sub);
        if (userIdClaim == null) return BadRequest();

        var user = await _userManager.FindByIdAsync(userIdClaim.Value);
        if (user == null) return BadRequest();

        return Ok(new 
        {
            Id = user.Id,
            UserName = user.UserName,
            Email = user.Email,
            PhoneNumber = user.PhoneNumber
        });
    }

    [HttpGet]
    public async Task<IActionResult> GetAllUsers()
    {
        var users = await _userManager.Users
        .Select(x => new {
            x.Id,
            x.Email,
            x.UserName,
            x.PhoneNumber
        })
        .ToListAsync();
        return Ok(users);
    }

}