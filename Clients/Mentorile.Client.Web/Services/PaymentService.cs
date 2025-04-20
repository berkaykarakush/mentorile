using Mentorile.Client.Web.Services.Abstracts;
using Mentorile.Client.Web.ViewModels.Payments;

namespace Mentorile.Client.Web.Services;
public class PaymentService : IPaymentService
{
    private readonly HttpClient _httpClient;

    public PaymentService(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    public async Task<bool> ReceivePaymentAsync(PaymentInfoInput paymentInfoInput)
    {
        var response = await _httpClient.PostAsJsonAsync<PaymentInfoInput>("payments", paymentInfoInput);
        return response.IsSuccessStatusCode;
    }
}