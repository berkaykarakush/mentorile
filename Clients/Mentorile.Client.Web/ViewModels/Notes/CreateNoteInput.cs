using System.ComponentModel.DataAnnotations;

namespace Mentorile.Client.Web.ViewModels.Notes;
public class CreateNoteInput
{
    public string UserId { get; set; }

    [Display(Name = "Not Başlığı")]
    public string Title { get; set; }

    [Display(Name = "Not İçeriği")]
    public string Content { get; set; }
}