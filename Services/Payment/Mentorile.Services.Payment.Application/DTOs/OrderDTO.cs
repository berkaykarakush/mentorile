namespace Mentorile.Services.Payment.Application.DTOs;
public class OrderDTO
{
    public string? BuyerId { get; set; }
    public string? OrderId { get; set; }
    public List<OrderItemDTO> OrderItems { get; set; } = new();
    public AddressDTO? Address { get; set; }       
}