using System.Threading.Tasks;
using Mentorile.Client.Web.Services.Abstracts;
using Mentorile.Client.Web.ViewModels.Orders;
using Microsoft.AspNetCore.Mvc;

namespace Mentorile.Client.Web.Controllers;
[Route("[controller]")]
public class OrdersController : Controller
{
    private readonly IBasketService _basketService;
    private readonly IOrderService _orderService;
    private readonly ILogger<OrdersController> _logger;

    public OrdersController(ILogger<OrdersController> logger, IBasketService basketService, IOrderService orderService)
    {
        _logger = logger;
        _basketService = basketService;
        _orderService = orderService;
    }

    [HttpGet("checkout")]
    public async Task<IActionResult> Checkout()
    {
        var basket = await _basketService.GetBasketAsync();
        ViewBag.basket = basket;
        return View(new CheckoutInfoInput());
    }

    [HttpPost("checkout")]
    public async Task<IActionResult> Checkout(CheckoutInfoInput checkoutInfoInput)
    {
        // 1. yol senkron iletisim
        // var orderStatus = await _orderService.CreateOrderAsync(checkoutInfoInput);
        // 2. yol asenkron iletisim
        var orderSuspend = await _orderService.SuspenOrderAsnyc(checkoutInfoInput);
        if(!orderSuspend.IsSuccessful){
            var basket = await _basketService.GetBasketAsync();
            ViewBag.basket = basket;
            ViewBag.error = orderSuspend.Error;
            return View();
        }

        // 1. yol senkron iletisim
        // return RedirectToAction(nameof(SuccessfulCheckout), new {orderId = orderStatus.OrderId});
        // TODO: Burada daha sonra OrderId degerini donecegiz
        return RedirectToAction(nameof(SuccessfulCheckout), new {orderId = Guid.NewGuid().ToString()});
    }

    [HttpGet("successful-checkout")]
    public IActionResult SuccessfulCheckout(string orderId)
    {
        // orderId'nin doğru şekilde geldiğini kontrol et
        if (string.IsNullOrEmpty(orderId))
        {
            _logger.LogError("Order ID is null or empty");
        }

        ViewData["orderId"] = orderId;
        return View();
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View("Error!");
    }

    [HttpGet("checkout-history")]
    public async Task<IActionResult> CheckoutHistory() 
        => View(await _orderService.GetOrdersAsync());
}