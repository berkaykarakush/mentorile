using Mentorile.Services.Course.DTOs.Course;
using Mentorile.Services.Course.Services;
using Mentorile.Shared.Common;
using Mentorile.Shared.ControllerBases;
using Microsoft.AspNetCore.Mvc;

namespace Mentorile.Services.Course.Controllers;

[ApiController]
[Route("api/[controller]")]
public class CoursesController : CustomControllerBase
{
    private readonly ICourseService _courseService;

    public CoursesController(ICourseService courseService)
    {
        _courseService = courseService;
    }

    [HttpGet]
    public async Task<IActionResult> GetAllAsync([FromQuery] PagingParams pagingParams)
    {
        var results = await _courseService.GetAllAsync(pagingParams);
        return CreateActionResultInstance(results);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetByIdAsync(string id)
    {
        var result = await _courseService.GetByIdAsync(id);
        return CreateActionResultInstance(result);
    }

    [HttpGet("GetAllByUserId/{userId}")]
    public async Task<IActionResult> GetAllByUserIdAsync(string userId)
    {
        var results = await _courseService.GetAllByUserIdAsync(userId);
        return CreateActionResultInstance(results);
    }

    [HttpPost]
    public async Task<IActionResult> CreateAsync(CreateCourseDTO createCourseDTO)
    {
        var result = await _courseService.CreateCourseAsync(createCourseDTO);
        return CreateActionResultInstance(result);
    }

    [HttpPut]
    public async Task<IActionResult> UpdateAsync(UpdateCourseDTO updateCourseDTO)
    {
        var result = await _courseService.UpdateCourseAsync(updateCourseDTO);
        return CreateActionResultInstance(result);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteAsync(string id)
    {
        var result = await _courseService.DeleteAsync(id);
        return CreateActionResultInstance(result);
    }
}