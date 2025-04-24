namespace Mentorile.Services.Discount.DTOs;
public class CreateDiscountDTO
{
    public List<string> UserIds { get; set; } = new();
    public int Rate { get; set; }
    public string Code { get; set; }
    public DateTime ExpirationDate { get; set; }
}