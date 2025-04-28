using MediatR;
using Mentorile.Services.Study.Application.Commands;
using Mentorile.Services.Study.Application.Interfaces;
using Mentorile.Services.Study.Application.Queries;
using Mentorile.Shared.Common;
using Mentorile.Shared.ControllerBases;
using Microsoft.AspNetCore.Mvc;

namespace Mentorile.Services.Study.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class StudiesController : CustomControllerBase
{
    private readonly IStudyAppService _studyAppService;
    private readonly IMediator _mediator;

    public StudiesController(IStudyAppService studyAppService, IMediator mediator)
    {
        _studyAppService = studyAppService;
        _mediator = mediator;
    }

    [HttpPost("create")]
    public async Task<IActionResult> Create(CreateStudyCommand command)
    {
        var response = await _mediator.Send(command);
        return CreateActionResultInstance(response);
    }

    [HttpPut("update")]
    public async Task<IActionResult> Update(UpdateStudyCommand command)
    {
        // command.Id = studyId;
        var response = await _mediator.Send(command);
        return CreateActionResultInstance(response);
    }

    [HttpDelete("delete/{studyId}")]
    public async Task<IActionResult> Delete(string studyId)
    {
        var deleteStudyCommand = new DeleteStudyCommand{ Id = studyId};
        var response = await _mediator.Send(deleteStudyCommand);
        return CreateActionResultInstance(response);
    }   

    [HttpGet("get-all")]
    public async Task<IActionResult> GetAll([FromQuery] PagingParams pagingParams)
    {
        var result = await _mediator.Send(new GetAllStudiesQuery() {PagingParams = pagingParams});
        return CreateActionResultInstance(result);
    }

    [HttpGet("user-get-all")]
    public async Task<IActionResult> GetAllUser(string userId, [FromQuery] PagingParams pagingParams)
    {
        var result = await _studyAppService.GetAllByUserIdAsync(userId, pagingParams);
        return CreateActionResultInstance(result);
    }

    [HttpGet("get-by-id/{studyId}")]
    public async Task<IActionResult> GetById(string studyId)
    {
        var result = await _studyAppService.GetByIdAsync(studyId);
        return CreateActionResultInstance(result);
    }
}