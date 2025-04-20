using System.ComponentModel.DataAnnotations;

namespace Mentorile.Client.Web.ViewModels.Orders;
public class CheckoutInfoInput
{
    [Display(Name = "İl")]
    [Required(ErrorMessage = "İl alanı boş bırakılamaz.")]
    public string Province { get; set; }    

    [Display(Name = "İlçe")]
    [Required(ErrorMessage = "İlçe alanı boş bırakılamaz.")]
    public string District { get; set; }

    [Display(Name = "Sokak")]
    [Required(ErrorMessage = "Sokak alanı boş bırakılamaz.")]
    public string Street { get; set; }
    
    [Display(Name = "Posta Kodu")]
    [Required(ErrorMessage = "Posta kodu boş bırakılamaz.")]
    public string ZipCode { get; set; }
    
    [Display(Name = "Adres")]
    [Required(ErrorMessage = "Adres satırı boş bırakılamaz.")]
    public string Line { get; set; }    
    
    [Display(Name = "Kart Üzerindeki İsim Soyisim")]
    [Required(ErrorMessage = "Kart üzerindeki isim boş bırakılamaz.")]
    public string CardName { get; set; }
    
    [Display(Name = "Kart Numarası")]
    [Required(ErrorMessage = "Kart numarası boş bırakılamaz.")]
    public string CardNumber { get; set; }
    
    [Display(Name = "Son Kullanma Tarihi Ay/Yıl")]
    [Required(ErrorMessage = "Son kullanma tarihi boş bırakılamaz.")]
    public string Expiration { get; set; }
    
    [Display(Name = "CVV/CVC2")]
    [Required(ErrorMessage = "CVV boş bırakılamaz.")]
    public string CVV { get; set; }
}