namespace Mentorile.Services.Note.Domain.Entities;

public class Note
{
    public Guid Id { get; private set; }
    public string UserId { get; private set; }
    public string Title { get; private set; }
    public string Content { get; private set; }
    public DateTime CreatedAt { get; private set; }
    public DateTime? UpdatedAt { get; private set; }
    public DateTime? DeletedAt { get; private set; }
    public bool IsDeleted { get; private set; }

    private Note() { }

    public Note(string userId, string title, string content)
    {
        Id = Guid.NewGuid();
        UserId = userId;
        Title = title;
        Content = content;
        CreatedAt = DateTime.UtcNow;
        IsDeleted = false;
    }

    public void Delete()
    {
        IsDeleted = true;
        UpdatedAt = DateTime.UtcNow;
        DeletedAt = DateTime.UtcNow;
    }

    public void Update(string title, string content)
    {
        Title = title;
        Content = content;
        UpdatedAt = DateTime.UtcNow;
    }
}