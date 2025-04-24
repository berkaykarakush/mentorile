namespace Mentorile.Services.Discount.DTOs;
public class UpdateDiscountDTO
{
    public string Id { get; set; }
    public List<string> UserIds { get; set; } = new();
    public int Rate { get; set; }
    public string Code { get; set; }
    public bool IsActive { get; set; } = true;
    public DateTime ExpirationDate { get; set; }
}