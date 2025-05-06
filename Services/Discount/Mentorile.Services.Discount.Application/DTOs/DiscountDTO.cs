namespace Mentorile.Services.Discount.Application.DTOs;
public class DiscountDTO
{
    public string Id { get; set; }  = string.Empty;
    public int Rate { get; set; }
    public string Code { get; set; } = string.Empty;
    public DateTime ExpirationDate { get; set; }
    public DateTime CreateAt { get; set; }
    public List<DiscountUserDTO> DiscountUsers { get; set; } = new();
}