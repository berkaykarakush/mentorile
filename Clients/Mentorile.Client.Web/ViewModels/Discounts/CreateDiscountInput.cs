using System.ComponentModel.DataAnnotations;

namespace Mentorile.Client.Web.ViewModels.Discounts;
public class CreateDiscountInput
{
    [Display(Name = "Kullanıcılar")]
    public List<string> UserIds { get; set; } = new();
    
    [Display(Name = "İndirim Oranı")]
    public int Rate { get; set; }
    
    [Display(Name = "Kupon Kodu")]
    public string Code { get; set; }
    
    [Display(Name = "Son Kullanma Tarihi (Gün/Ay/Yıl)")]
    public DateTime ExpirationDate { get; set; }
}