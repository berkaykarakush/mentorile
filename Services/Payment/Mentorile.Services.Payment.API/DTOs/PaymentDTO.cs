using System.ComponentModel.DataAnnotations;

namespace Mentorile.Services.Payment.API.DTOs;
public class PaymentDTO
{
    [Required]
    public string CardName { get; set; }    
    
    [Required]
    public string CardNumber { get; set; }

    [Required]
    public string Expiration { get; set; }

    [Required]
    public string CVV { get; set; }

    [Required]
    public decimal TotalPrice { get; set; }    

    public OrderDTO Order { get; set; }
}