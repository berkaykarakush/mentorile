namespace Mentorile.Services.User.Domain.Entities;
public class Role
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    // Rolün açıklaması
    public string Description { get; set; } = null!;

    // Rolün aktif olup olmadığını belirten bir bayrak
    public bool IsActive { get; set; } = true;

    // Rolün oluşturulma tarihi
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

    // Rolün güncellenme tarihi
    public DateTime? UpdatedAt { get; set; }

    // Rollere özel izinler ya da ek bilgiler (isteğe bağlı)
    public string? Permissions { get; set; } // JSON veya benzeri bir koleksiyon olabilir
}