namespace Mentorile.Services.Discount.Domain.Entities;
public class Discount
{
    public string Id { get; set; } = Guid.NewGuid().ToString();
    public int Rate { get; set; }
    public string Code { get; set; } = string.Empty;
    public DateTime ExpirationDate { get; set; }
    public bool IsActive { get; set; } = true;
    public DateTime CreateAt { get; set; }
    public DateTime? UpdatedAt { get; set; }
    public DateTime? DeletedAt { get; set; }
    public bool IsDeleted { get; set; } = false;
    public List<DiscountUser> DiscountUsers { get; set; } = new();
    public void Delete()
    {
        IsDeleted = true;
        IsActive = false;
        UpdatedAt = DateTime.UtcNow;
        DeletedAt = DateTime.UtcNow;
    }
}