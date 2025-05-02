using System.ComponentModel.DataAnnotations;

namespace Mentorile.Client.Web.Models;
public class RegisterInput
{
    [Required]
    [Display(Name = "İsim")]
    public string Name { get; set; }

    [Required]
    [Display(Name = "Soyisim")]
    public string Surname { get; set; }

    [Required]
    [Display(Name = "Email adresiniz")]
    public string Email { get; set; }

    [Required]
    [Display(Name = "Telefon Numarası")]
    public string PhoneNumber { get; set; }

    [Required]
    [Display(Name = "Şifreniz")]
    public string Password { get; set; }

     [Required]
    [Display(Name = "Şifre Tekrarı")]
    public string RePassword { get; set; }
}