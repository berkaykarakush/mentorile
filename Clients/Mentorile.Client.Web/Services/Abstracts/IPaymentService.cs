using Mentorile.Client.Web.ViewModels.Payments;

namespace Mentorile.Client.Web.Services.Abstracts;
public interface IPaymentService
{
    Task<string> CreatePaymentAsync(CreatePaymentInput createPaymentInput);
    Task<string> ReceivePaymentAsync(PaymentInfoInput paymentInfoInput);    
}