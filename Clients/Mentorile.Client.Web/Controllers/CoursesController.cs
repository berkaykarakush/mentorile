using Mentorile.Client.Web.Services.Abstracts;
using Mentorile.Client.Web.ViewModels.Courses;
using Mentorile.Shared.Common;
using Mentorile.Shared.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Mentorile.Client.Web.Controllers;

[Authorize]
[Route("[controller]")]
public class CoursesController : Controller
{
    private readonly ICourseService _courseService;
    private readonly ISharedIdentityService _sharedIdentityService;
    private readonly ILogger<CoursesController> _logger;

    public CoursesController(ILogger<CoursesController> logger, ICourseService courseService, ISharedIdentityService sharedIdentityService)
    {
        _logger = logger;
        _courseService = courseService;
        _sharedIdentityService = sharedIdentityService;
    }
    [HttpGet("index")]
    public async Task<IActionResult> Index(PagingParams pagingParams)
    {
        var courses = await _courseService.GetAllCourseByUserIdAsync(_sharedIdentityService.GetUserId);
        ViewBag.Paging = pagingParams;
        if(courses == null){
            var emptyResult = new PagedResult<CourseViewModel>(
                new List<CourseViewModel>(),
                0,
                pagingParams,
                200,
                "No data available"
            );
            return View(emptyResult);
        }

        var totalCount = courses.TotalCount;
        var statusCode = 200;
        var message = "Success";

        var listCourses = new List<CourseViewModel>();
        foreach (var item in courses.Data)
            listCourses.Add(new CourseViewModel(){
                Id = item.Id,
                Name = item.Name,
                UserId = item.UserId,
                PhotoUri = item.PhotoUri,
                CreatedTime = item.CreatedTime,
                TopicIds = item.TopicIds
            });

        var pagedResult = new PagedResult<CourseViewModel>(
            listCourses,
            totalCount,
            pagingParams,
            statusCode,
            message
        );        
        return View(pagedResult);
    }

    [HttpGet("create")]
    public async Task<IActionResult> Create() => View();

    [HttpPost("create")]
    public async Task<IActionResult> Create(CreateCourseInput createCourseInput)
    {
        // select list // 
        // var categories = await _courseService.GetAllCourseAsync();
        //  ViewBag.categoryList = new SelectList(categories, "Id", "Name");
        createCourseInput.UserId = _sharedIdentityService.GetUserId;
        if(!ModelState.IsValid) return View(createCourseInput);

        await _courseService.CreateCourseAsync(createCourseInput);
        return RedirectToAction(nameof(Index));
    }
    [HttpGet("update")]
    public async Task<IActionResult> Update(string id)
    {
        var course = await _courseService.GetCourseByIdAsync(id);
        var topicIds = await _courseService.GetAllCourseAsync();

        if (course == null)
        {
            //mesaj göster
            RedirectToAction(nameof(Index));
        }
        ViewBag.Topics = new SelectList(topicIds.Data, "Id", "Name", course.Id);
        UpdateCourseInput updateCourseInput = new()
        {
            Id = course.Id,
            Name = course.Name,
            UserId = course.UserId,
            PhotoUri = course.PhotoUri,
            TopicIds = course.TopicIds
        };
        return View(updateCourseInput);
    }

    [HttpPost("update")]
    public async Task<IActionResult> Update(UpdateCourseInput updateCourseInput)
    {
        if (!ModelState.IsValid)
        {
            // ViewBag.Topics tekrar set edilmeli
            var allTopics = await _courseService.GetAllCourseAsync(); 
            ViewBag.Topics = allTopics.Data.Select(t => new SelectListItem
            {
                Value = t.Id,
                Text = t.Name
            }).ToList();

            return View(updateCourseInput);
        }

        var existingCourse = await _courseService.GetCourseByIdAsync(updateCourseInput.Id);
        if (existingCourse == null)
        {
            return RedirectToAction(nameof(Index));
        }

        await _courseService.UpdateCourseAsync(updateCourseInput);
        return RedirectToAction(nameof(Index));
    }
    [HttpGet("delete")]
    public async Task<IActionResult> Delete(string id)
    {
        await _courseService.DeleteCourseAsync(id);
        return RedirectToAction(nameof(Index));
    }
}