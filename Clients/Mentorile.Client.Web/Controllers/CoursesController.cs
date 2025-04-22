using Mentorile.Client.Web.Services.Abstracts;
using Mentorile.Client.Web.ViewModels.Courses;
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
    public async Task<IActionResult> Index()
    {
        var courses = await _courseService.GetAllCourseByUserIdAsync(_sharedIdentityService.GetUserId);
        return View(courses ?? new List<CourseViewModel>());
    }

    [HttpGet("create")]
    public async Task<IActionResult> Create() => View();
    // select list olusturmak icin //
    // {
    //     var categories = await _courseService.GetAllCourseAsync();
    //     ViewBag.categoryList = new SelectList(categories, "Id", "Name");
    //     return View(categories);
    // }

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
        ViewBag.Topics = new SelectList(topicIds, "Id", "Name", course.Id);
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
            ViewBag.Topics = allTopics.Select(t => new SelectListItem
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