using Mentorile.Client.Web.ViewModels.Orders;

namespace Mentorile.Client.Web.Services.Abstracts;
public interface IOrderService
{
    // senkron iletisim direkt olarak microservice istek yapilacak
    Task<OrderCreatedViewModel> CreateOrderAsync(CheckoutInfoInput checkoutInfoInput);    
    // asenkron iletisim siparis bilgileri rabbitmq ya gonderilecek
    Task<OrderSuspendViewModel> SuspenOrderAsnyc(CheckoutInfoInput checkoutInfoInput);
    Task<List<OrderViewModel>> GetOrdersAsync();
}