using Mentorile.Client.Web.Services.Abstracts;
using Mentorile.Client.Web.ViewModels.Studies;
using Mentorile.Shared.Common;
using Mentorile.Shared.Services;
using Microsoft.AspNetCore.Mvc;

namespace Mentorile.Client.WebControllers;
[Route("[controller]")]

public class StudiesController : Controller
{
    private readonly ILogger<StudiesController> _logger;
    private readonly IStudyService _studyService;
    private readonly ISharedIdentityService _sharedIdentityService;
    public StudiesController(ILogger<StudiesController> logger, IStudyService studyService, ISharedIdentityService sharedIdentityService)
    {
        _logger = logger;
        _studyService = studyService;
        _sharedIdentityService = sharedIdentityService;
    }
    
    [HttpGet("user-get-all")]
    public async Task<IActionResult> Index(PagingParams pagingParams)
    {
        var studies = await _studyService.GetAllByUserIdAsync(_sharedIdentityService.GetUserId);
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
    
    [HttpGet("create")]
    public async Task<IActionResult> Create() => View();


    [HttpPost("create")]
    public async Task<IActionResult> Create(CreateStudyInput createStudyInput)
    {
        if(!ModelState.IsValid) return View(createStudyInput);
        await _studyService.CreateStudyAsync(createStudyInput);
        return RedirectToAction(nameof(Index));
    }


    [HttpGet("update")]
    public async Task<IActionResult> Update(string studyId)
    {
        var study = await _studyService.GetByIdAsync(studyId);
        if(study == null) return RedirectToAction(nameof(Index));
        var updateStudyInput = new UpdateStudyInput(){
            Id = study.Id,
            UserId = study.UserId,
            Name = study.Name
        }; 
        return View(updateStudyInput);
    } 

    [HttpPost("update")]
    public async Task<IActionResult> Update(UpdateStudyInput updateStudyInput)
    {
        if(!ModelState.IsValid) return View(updateStudyInput);
        await _studyService.UpdateStudyAsync(updateStudyInput);
        return RedirectToAction(nameof(Index));
    } 

    [HttpGet("delete")]
    public async Task<IActionResult> Delete(string studyId)
    {
        await _studyService.DeleteStudyAsync(studyId);
        return RedirectToAction(nameof(Index));
    }
}
