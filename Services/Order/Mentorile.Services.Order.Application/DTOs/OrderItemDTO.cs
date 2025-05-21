namespace Mentorile.Services.Order.Application.DTOs;
public class OrderItemDTO
{
    public string ItemId { get; set; }    
    public string ItemName { get; set; }
    public string PhotoUri { get; set; }
    public decimal Price { get; set; }
}