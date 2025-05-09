using System.Text.Json;
using Mentorile.Client.Web.Services.Abstracts;
using Mentorile.Client.Web.ViewModels.Payments;
using Mentorile.Shared.Common;
using Mentorile.Shared.Messages.Commands;
using Microsoft.IdentityModel.Tokens;

namespace Mentorile.Client.Web.Services;
public class PaymentService : IPaymentService
{
    private const string controllersName = "payments";
    private readonly HttpClient _httpClient;

    public PaymentService(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    public async Task<string> CreatePaymentAsync(CreatePaymentInput createPaymentInput)
    {
        var response = await _httpClient.PostAsJsonAsync<CreatePaymentInput>($"{controllersName}", createPaymentInput);
        if(!response.IsSuccessStatusCode) return null;
        var result = await response.Content.ReadFromJsonAsync<Result<string>>();
        return result.Data;
    }

    public async Task<string> ReceivePaymentAsync(PaymentInfoInput paymentInfoInput)
    {
        // TODO: Gercek odeme saglayicinsa gonderilecek data PaymentInfoInput ile gonderilecek
        // var paymentResponse = await _httpClient.PostAsJsonAsync<PaymentInfoInput>($"{controllersName}/receive", paymentInfoInput);
        // if(paymentResponse.IsSuccess)
        // {

        // }

        List<string> cardTypes = ["CreditCard", "BankCard"];
        var type = cardTypes[new Random().Next(0,2)];
        var receivePaymentInput = new ReceivePaymentInput()
        {
            OrderId = paymentInfoInput.Order.OrderId,
            UserId = paymentInfoInput.Order.BuyerId,
            PaymentId = paymentInfoInput.Order.PaymentId,
            TotalPrice = paymentInfoInput.TotalPrice,
            Currency = "TRY",
            PaymentStatus = "Completed",
            PaymentMethod = type,
            TransactionId = Guid.NewGuid().ToString(),
            CardMaskedNumber = $"**** **** **** **** {new Random().Next(1000, 9999)}",
            CardType = type,
            GatewayResponse = "Success",
            ClientIpAddress = $"192.{new Random().Next(100,999)}.1.1",
            Order = paymentInfoInput.Order,
        };
        var jsonString = JsonSerializer.Serialize(receivePaymentInput, new JsonSerializerOptions
        {
            WriteIndented = true
        });
        Console.WriteLine(jsonString);
        var response = await _httpClient.PostAsJsonAsync<ReceivePaymentInput>($"{controllersName}/receive", receivePaymentInput);
        if(!response.IsSuccessStatusCode) return null;

        var result = await response.Content.ReadFromJsonAsync<Result<string>>();
        return result.Data;
    }
}