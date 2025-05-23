namespace Mentorile.Services.Email.Domain.Entities;
public class EmailMessage
{
    public string Id { get; set; } = Guid.NewGuid().ToString();
    public string To { get; set; } = default!;
    public string Subject { get; set; } = default!;
    public string Body { get; set; } = default!;
}