using Mentorile.Client.Web.Services.Abstracts;
using Mentorile.Client.Web.ViewModels.Studies;
using Mentorile.Shared.Common;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Mentorile.Client.Web.Areas.Admin.Controllers;

[Authorize]
[Area("Admin")]
[Route("Admin/[controller]")]
public class StudiesController : Controller
{
    private readonly ILogger<StudiesController> _logger;
    private readonly IStudyService _studyService;
    public StudiesController(ILogger<StudiesController> logger, IStudyService studyService)
    {
        _logger = logger;
        _studyService = studyService;
    }

    [HttpGet("get-all")]
    public async Task<IActionResult> Index(PagingParams pagingParams)
    {
        var studies = await _studyService.GetAllAsync(pagingParams);
        ViewBag.Paging = pagingParams;
        if(studies == null){
            // Eğer studies null ise, boş bir PagedResult döndürüyoruz
            var emptyResult = new PagedResult<StudyViewModel>(
                new List<StudyViewModel>(), // Boş bir liste
                0, // Toplam öğe sayısı 0
                pagingParams, // Sayfalama bilgileri
                200, // Durum kodu
                "No data available" // Mesaj
            );
            return View(emptyResult);
        }

        var totalCount = studies.TotalCount; // Toplam öğe sayısı
        var statusCode = 200; // Durum kodu
        var message = "Success"; // Mesaj

        var listStudies = new List<StudyViewModel>();
        foreach (var item in studies.Data)
        {  
            listStudies.Add(new StudyViewModel(){
                Id = item.Id,
                Name = item.Name,
                UserId = item.UserId,
                CreatedDate = item.CreatedDate
            });
        }
        var pagedResult = new PagedResult<StudyViewModel>(
            listStudies,
            totalCount, // Toplam öğe sayısı
            pagingParams, // Sayfalama bilgileri
            statusCode, // Durum kodu
            message // Mesaj
        );
        return View(pagedResult);
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View("Error!");
    }
}