using System.ComponentModel.DataAnnotations;

namespace Mentorile.Client.Web.ViewModels.Discounts;
public class UpdateDiscountInput
{
    [Display(Name = "Kurs Id")]
    public string Id { get; set; }

    [Display(Name = "Kullanıcılar")]
    public List<string> UserIds { get; set; } = new();

    [Display(Name = "İndirim Oranı")]
    public int Rate { get; set; }

    [Display(Name = "Kupon Kodu")]
    public string Code { get; set; }

    [Display(Name = "Aktiflik Durumu")]
    public bool IsActive { get; set; } = true;

    [Display(Name = "Son Kullanma Tarihi (Gün/Ay/Yıl)")]
    public DateTime ExpirationDate { get; set; }
}