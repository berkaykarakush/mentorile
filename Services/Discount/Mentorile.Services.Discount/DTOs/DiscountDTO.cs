namespace Mentorile.Services.Discount.DTOs;
public class DiscountDTO
{
    public string Id { get; set; }
    public string UserId { get; set; }
    public int Rate { get; set; }
    public string Code { get; set; }
    public bool IsActive { get; set; } = true;
    public DateTime CreatedDate { get; set; }    
}