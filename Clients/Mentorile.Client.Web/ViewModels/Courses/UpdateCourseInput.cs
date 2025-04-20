using System.ComponentModel.DataAnnotations;

namespace Mentorile.Client.Web.ViewModels.Courses;
public class UpdateCourseInput
{
    public string Id { get; set; }

    [Display(Name = "Kurs İsmi")]
    public string Name { get; set; }

    [Display(Name = "Kullanıcı id")]
    public string UserId { get; set; }

    [Display(Name = "Resim Url")]
    public string? PhotoUri { get; set; }

    [Display(Name = "Resim")]
    public IFormFile? PhotoFormFile { get; set; }

    public List<string> TopicIds { get; set; } = new List<string>();
}