namespace Mentorile.Services.Discount.DTOs;
public class CreateDiscountDTO
{
    public string UserId { get; set; }
    public int Rate { get; set; }
    public string Code { get; set; }
}