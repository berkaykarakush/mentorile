namespace Mentorile.Services.Payment.API.DTOs;
public class OrderDTO
{
      public string BuyerId { get; set; }
    public List<OrderItemDTO> OrderItems { get; set; } = new();
    public AddresDTO Address { get; set; }       
}