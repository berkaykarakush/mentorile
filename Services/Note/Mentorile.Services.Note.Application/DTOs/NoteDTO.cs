namespace Mentorile.Services.Note.Application.DTOs;
public class NoteDTO
{
    public required string Id { get; set; }
    public required string UserId { get; set; }

    public required string Title { get; set; }
    public required string Content { get; set; }

    public DateTime CreatedAt { get; set; }
    public DateTime? UpdatedAt { get; set; }
}