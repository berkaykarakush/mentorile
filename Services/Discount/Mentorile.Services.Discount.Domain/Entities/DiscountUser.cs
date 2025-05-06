namespace Mentorile.Services.Discount.Domain.Entities;
public class DiscountUser
{
    public string DiscountId { get; set; }    
    public Discount Discount { get; set; }
    public string UserId { get; set; }
}