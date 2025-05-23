namespace Mentorile.Services.Email.Domain.Entities;

public class EmailLog
{
    public string Id { get; set; } = Guid.NewGuid().ToString();
    public string To { get; set; } = default!;
    public string Subject { get; set; } = default!;
    public string Body { get; set; } = default!;
    public bool IsSuccess { get; set; }
    public string? ErrorMessage { get; set; }
    public DateTime SentAt { get; set; } = DateTime.UtcNow;    
}