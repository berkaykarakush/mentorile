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
        var orderStatus = await _orderService.CreateOrderAsync(checkoutInfoInput);
        if(!orderStatus.IsSuccessful){
            var basket = await _basketService.GetBasketAsync();
            ViewBag.basket = basket;
            ViewBag.error = orderStatus.Error;
            return View();
        }
        return RedirectToAction(nameof(SuccessfulCheckout), new {orderId = orderStatus.OrderId});
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
}