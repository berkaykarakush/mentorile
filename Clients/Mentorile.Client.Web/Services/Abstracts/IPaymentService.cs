using Mentorile.Client.Web.ViewModels.Payments;

namespace Mentorile.Client.Web.Services.Abstracts;
public interface IPaymentService
{
    Task<bool> ReceivePaymentAsync(PaymentInfoInput paymentInfoInput);    
}