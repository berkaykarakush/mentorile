using Mentorile.Services.PhotoStock.Domain.Enums;

namespace Mentorile.Services.PhotoStock.Domain.Entities;
public class Photo
{
    public string Id { get; set; } = Guid.NewGuid().ToString();    
    public string? UserId { get; set; }
    // IsPublic == true ise doldur
    public string? PublicUri { get; set; }
    public string FileName { get; set; }
    // IsPublic == false ise doldur
    public string? StoragePath { get; set; }
    public string ContentType { get; set; }
    // public PhotoType PhotoType { get; set; }
    public bool IsPublic { get; set; }
    public DateTime CreateAt { get; set; } = DateTime.UtcNow;
    public bool IsDeleted { get; set; } = false;
    public DateTime? DeletedAt { get; set; }

    public void Delete()
    {
        IsDeleted = true;
        DeletedAt = DateTime.UtcNow;
    }
}