using System.ComponentModel.DataAnnotations;

namespace Mentorile.Client.Web.ViewModels.Courses;
public class CreateCourseInput
{
    [Display(Name = "Kurs İsmi")]
    public string Name { get; set; } = string.Empty;

    [Display(Name = "Kullanıcı id")]
    public string UserId { get; set; } = string.Empty;

    [Display(Name = "Resim Url")]
    public string? PhotoUri { get; set; }
    
    [Display(Name = "Resim")]
    public IFormFile? PhotoFormFile { get; set; }
}