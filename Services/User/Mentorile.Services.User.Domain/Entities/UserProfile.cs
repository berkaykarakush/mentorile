using Mentorile.Services.User.Domain.Enums;

namespace Mentorile.Services.User.Domain.Entities;
public class UserProfile
{
    public Guid Id { get; set; }
    public string Name { get; set; } = null!;
    public string Surname { get; set; } = null!;
    public string Email { get; set; } = null!;
    public string? PhoneNumber { get; set; }
    public UserStatus Status { get; set; } = UserStatus.Pending;
    public DateTime CreateAt { get; set; }
    public DateTime? UpdatedAt { get; set; }
    public DateTime? DeletedAt { get; set; }
    public bool IsDeleted { get; set; } = false;
 
    public void Activate() => Status = UserStatus.Active;
    // TODO: Email onay ve Phone onay icerisinde kullanici durumu active olacak
    public void Inactivate() => Status = UserStatus.Inactive;
    // TODO: Kullaniciyi banlamak icin metot olmasi gerekiyor
    public void Ban() => Status = UserStatus.Banned;
    public void Delete()
    {
        IsDeleted = true;
        Status = UserStatus.Inactive;
        DeletedAt = DateTime.UtcNow;
        UpdatedAt = DateTime.UtcNow;
    }
}