using Mentorile.Client.Web.Services.Abstracts;
using Mentorile.Client.Web.ViewModels.Orders;
using Mentorile.Client.Web.ViewModels.Payments;
using Mentorile.Shared.Common;
using Mentorile.Shared.Services;
using Microsoft.VisualBasic;

namespace Mentorile.Client.Web.Services;
public class OrderService : IOrderService
{
    private readonly HttpClient _httpClient;
    private readonly IPaymentService _paymentService;
    private readonly IBasketService _basketService;
    private readonly ISharedIdentityService _sharedIdentityService;

    public OrderService(HttpClient httpClient, IPaymentService paymentService, IBasketService basketService, ISharedIdentityService sharedIdentityService)
    {
        _httpClient = httpClient;
        _paymentService = paymentService;
        _basketService = basketService;
        _sharedIdentityService = sharedIdentityService;
    }

    public async Task<OrderCreatedViewModel> CreateOrderAsync(CheckoutInfoInput checkoutInfoInput)
    {
        var basket = await _basketService.GetBasketAsync();
        var paymentInfoInput = new PaymentInfoInput(){
            CardName = checkoutInfoInput.CardName,
            CardNumber = checkoutInfoInput.CardNumber,
            Expiration = checkoutInfoInput.Expiration,
            CVV = checkoutInfoInput.CVV,
            TotalPrice = basket.FinalAmount
        };
        var responsePayment = await _paymentService.ReceivePaymentAsync(paymentInfoInput);
        if(responsePayment == null) return new OrderCreatedViewModel() { Error = "Ödeme alınamadı.", IsSuccessful = false};

        var addressCreateInput = new AddressCreateInput(){
            Province = checkoutInfoInput.Province,
            District = checkoutInfoInput.District,
            Street = checkoutInfoInput.Street,
            ZipCode = checkoutInfoInput.ZipCode,
            Line = checkoutInfoInput.Line
        };

        var orderCreateInput = new OrderCreateInput(){
            BuyerId = _sharedIdentityService.GetUserId.ToString(),
            Address = addressCreateInput,
        };

        basket.Items.ForEach(x => {
            var orderItem = new OrderItemCreateInput() { ItemId = x.ItemId, ItemName = x.ItemName, PictureUri = "", Price = x.Price};
            orderCreateInput.OrderItems.Add(orderItem);
        });

        var response = await _httpClient.PostAsJsonAsync<OrderCreateInput>("orders", orderCreateInput);

        if (!response.IsSuccessStatusCode)
        {
            var errorContent = await response.Content.ReadAsStringAsync();

            return new OrderCreatedViewModel
            {
                Error = $"Sipariş oluşturulamadı. Detay: {errorContent}",
                IsSuccessful = false
            };
        }
        // API'den gelen yanıtı Result<OrderCreatedDTO> formatında alıyoruz
        var result = await response.Content.ReadFromJsonAsync<Result<OrderCreatedViewModel>>();

        // Eğer hata varsa, error mesajı ile birlikte view model döndürüyoruz
        if (result == null && result.IsSuccess && result.Data == null) return new OrderCreatedViewModel{ Error = "Sipariş oluşturulurken bir hata oluştu.", IsSuccessful = false};
        
        await _basketService.ClearBasketAsync();
        return new OrderCreatedViewModel {IsSuccessful = true, OrderId = result.Data.OrderId};;
    }

    public async Task<List<OrderViewModel>> GetOrdersAsync()
    {
        var response = await _httpClient.GetFromJsonAsync<Result<List<OrderViewModel>>>("orders");
        return response.Data;
    }

    public async Task<OrderSuspendViewModel> SuspenOrderAsnyc(CheckoutInfoInput checkoutInfoInput)
    {
        var basket = await _basketService.GetBasketAsync();

        var addressCreateInput = new AddressCreateInput(){
            Province = checkoutInfoInput.Province,
            District = checkoutInfoInput.District,
            Street = checkoutInfoInput.Street,
            ZipCode = checkoutInfoInput.ZipCode,
            Line = checkoutInfoInput.Line
        };

        var orderCreateInput = new OrderCreateInput(){
            BuyerId = _sharedIdentityService.GetUserId.ToString(),
            Address = addressCreateInput,
        };

        basket.Items.ForEach(x => {
            var orderItem = new OrderItemCreateInput() { ItemId = x.ItemId, ItemName = x.ItemName, PictureUri = "", Price = x.Price};
            orderCreateInput.OrderItems.Add(orderItem);
        });

        var paymentInfoInput = new PaymentInfoInput(){
            CardName = checkoutInfoInput.CardName,
            CardNumber = checkoutInfoInput.CardNumber,
            Expiration = checkoutInfoInput.Expiration,
            CVV = checkoutInfoInput.CVV,
            TotalPrice = basket.FinalAmount,
            Order = orderCreateInput
        };

        var responsePayment = await _paymentService.CreatePaymentAsync(new CreatePaymentInput { UserId = paymentInfoInput.Order.BuyerId, TotalPrice = paymentInfoInput.TotalPrice});
        if(responsePayment != null)
        {
            paymentInfoInput.Order.PaymentId = responsePayment;
            var receivePayment = await _paymentService.ReceivePaymentAsync(paymentInfoInput);
            if(receivePayment == null) return new OrderSuspendViewModel() { Error = "Ödeme alınamadı.", IsSuccessful = false};
            
            await _basketService.ClearBasketAsync();
            return new OrderSuspendViewModel() { OrderId = paymentInfoInput.Order.OrderId , Error = "Ödeme başarılı.", IsSuccessful = true};
        }
        return new OrderSuspendViewModel() { Error = "Ödeme alınamadı.", IsSuccessful = true};
    }
}