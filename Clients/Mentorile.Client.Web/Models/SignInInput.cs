using System.ComponentModel.DataAnnotations;

namespace Mentorile.Client.Web.Models;
public class SignInInput
{
    [Required]
    [Display(Name = "Email adresiniz")]
    public string Email { get; set; }
    
    [Required]
    [Display(Name = "Sifreniz")]
    public string Password { get; set; }

    [Display(Name = "Beni hatirla")]
    public bool IsRemember { get; set; }
}