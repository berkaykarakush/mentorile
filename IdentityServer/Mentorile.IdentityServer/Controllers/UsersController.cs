using System.IdentityModel.Tokens.Jwt;
using Mentorile.IdentityServer.DTOs;
using Mentorile.IdentityServer.Models;
using Mentorile.Shared.Common;
using Mentorile.Shared.ControllerBases;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using static Duende.IdentityServer.IdentityServerConstants;

namespace Mentorile.IdentityServer.Controllers;

[Authorize(LocalApi.PolicyName)]
[ApiController]
[Route("api/[controller]/[action]")]
public class UsersController : CustomControllerBase
{
    private readonly UserManager<ApplicationUser> _userManager;
        // private readonly IPasswordValidator<ApplicationUser> _passwordValidator;
    private readonly IPasswordValidator<ApplicationUser> _passwordValidator;

    public UsersController(UserManager<ApplicationUser> userManager, IPasswordValidator<ApplicationUser> passwordValidator)
    {
        _userManager = userManager;
        _passwordValidator = passwordValidator;
    }

    [HttpPost]
    public async Task<IActionResult> SignUp([FromBody]SignUpDTO signUpDTO)
    {

        var user = new ApplicationUser()
        {
            // example mentorile@2233-ss23-s242-xxxx
            UserName = $"{signUpDTO.Email.Split('@')[0]}@{Guid.NewGuid()}",
            Email = signUpDTO.Email,
            PhoneNumber = signUpDTO.PhoneNumber,
        };

        // Validate the password before creating the user
        var passwordValidationResult = await _passwordValidator.ValidateAsync(_userManager, user, signUpDTO.Password);
        if (!passwordValidationResult.Succeeded)
        {
            var errors = passwordValidationResult.Errors.Select(e => e.Description).ToList();
            var validationResult = Result<string>.Failure(errors);
            return CreateActionResultInstance(validationResult);
        }

        // Create the user
        var identityResult = await _userManager.CreateAsync(user);

        if(!identityResult.Succeeded)
        {
            var erros = identityResult.Errors.Select(e => e.Description).ToList();
            var failureResult = Result<string>.Failure(erros);
            return CreateActionResultInstance(failureResult);
        }

        var successResult = Result<string>.Success("User created successfully.");
        return CreateActionResultInstance(successResult);
    }

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