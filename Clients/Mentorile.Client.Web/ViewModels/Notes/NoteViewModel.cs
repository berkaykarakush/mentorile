namespace Mentorile.Client.Web.ViewModels.Notes;
public class NoteViewModel
{
    public string Id { get; set; }
    public string UserId { get; set; }
    public string Title { get; set; }
    public string Content { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime? UpdatedAt { get; set; }
}