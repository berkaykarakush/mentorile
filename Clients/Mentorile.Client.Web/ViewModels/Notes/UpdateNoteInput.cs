using System.ComponentModel.DataAnnotations;

namespace Mentorile.Client.Web.ViewModels.Notes;
public class UpdateNoteInput
{
    [Display(Name = "Not Id")]
    public string Id { get; set; }

    public string UserId { get; set; }
    [Display(Name = "Not Başlığı")]
    public string Title { get; set; }

    [Display(Name = "Not İçeriği")]
    public string Content { get; set; }
}