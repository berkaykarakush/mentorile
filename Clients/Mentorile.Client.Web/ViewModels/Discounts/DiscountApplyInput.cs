using System.ComponentModel.DataAnnotations;

namespace Mentorile.Client.Web.ViewModels.Discounts;
public class DiscountApplyInput
{
    [Display(Name = "İndirim kodu")]
    public string Code { get; set; }    
    public decimal TotalPrice { get; set; }
}