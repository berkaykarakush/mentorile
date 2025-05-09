namespace Mentorile.Services.Payment.Application.DTOs;
public class OrderItemDTO
{
    public string? ItemId { get; set; }    
    public string? ItemName { get; set; }
    public string? PictureUri { get; set; }
    public decimal Price { get; set; }   
}