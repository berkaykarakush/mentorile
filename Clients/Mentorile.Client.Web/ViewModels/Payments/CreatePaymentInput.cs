namespace Mentorile.Client.Web.ViewModels.Payments;
public class CreatePaymentInput
{
    public string UserId { get; set; } = string.Empty;
    public decimal TotalPrice { get; set; }
    public string Currency { get; set; } = "TRY";
}