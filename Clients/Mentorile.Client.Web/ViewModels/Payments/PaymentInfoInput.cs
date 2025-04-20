using System.ComponentModel.DataAnnotations;
using Mentorile.Client.Web.ViewModels.Orders;

namespace Mentorile.Client.Web.ViewModels.Payments;
public class PaymentInfoInput
{
    [Required, Display(Name = "Kart Sahibi Adı")]
    // [StringLength(100, MinimumLength = 2)]
    public string CardName { get; set; }

    [Required, Display(Name = "Kart Numarası")]
    // [CreditCard]  // Basic format check
    // [StringLength(19, MinimumLength = 13)] // Visa: 13–19 haneli olabilir
    public string CardNumber { get; set; }

    [Required, Display(Name = "Son Kullanma Tarihi")]
    // [RegularExpression(@"^(0[1-9]|1[0-2])\/?([0-9]{2})$", ErrorMessage = "Tarih MM/YY formatında olmalıdır")]
    public string Expiration { get; set; }

    [Required, Display(Name = "CVV")]
    // [RegularExpression(@"^\d{3,4}$", ErrorMessage = "CVV 3 ya da 4 haneli olmalıdır")]
    public string CVV { get; set; }

    [Required, Range(0.01, double.MaxValue, ErrorMessage = "Toplam tutar geçerli olmalıdır")]
    public decimal TotalPrice { get; set; }

    public OrderCreateInput Order { get; set; }
}