using MediatR;
using Mentorile.Services.Course.Application.Commands;
using Mentorile.Services.Course.Application.Queries;
using Mentorile.Shared.Common;
using Mentorile.Shared.ControllerBases;
using Microsoft.AspNetCore.Mvc;

namespace Mentorile.Services.Course.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class CoursesController : CustomControllerBase
{
    private readonly IMediator _mediator;

    public CoursesController(IMediator mediator)
    {
        _mediator = mediator;
    }

    // [HttpGet]
    // public async Task<IActionResult> GetAll(GetAllCoursesQuery query)
    //     => CreateActionResultInstance(await _mediator.Send(query));

    // [HttpGet("by-id")]
    // public async Task<IActionResult> GetById(string id)
    //     => CreateActionResultInstance(await _mediator.Send(new GetCourseByIdQuery{ Id = id}));
    
    [HttpGet("user/{userId}")]
    public async Task<IActionResult> GetByUserId(string userId)
    {
        var query = new GetAllCoursesByUserIdQuery{ UserId = userId};
        // query.UserId = userId;
        return CreateActionResultInstance(await _mediator.Send(query));
    }

    [HttpPost]
    public async Task<IActionResult> CreateCourse(CreateCourseCommand command)
        => CreateActionResultInstance(await _mediator.Send(command));

    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateCourse(string id, UpdateCourseCommand command)
    {
        command.Id = id;
        return CreateActionResultInstance(await _mediator.Send(command));
    }

    [HttpDelete]
    public async Task<IActionResult> DeleteCourse(string id)
        => CreateActionResultInstance(await _mediator.Send(new DeleteCourseCommand{ Id = id}));

}