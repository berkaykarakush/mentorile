using Mentorile.Client.Web.Services.Abstracts;
using Mentorile.Client.Web.ViewModels.Baskets;
using Microsoft.AspNetCore.Mvc;

namespace Mentorile.Client.Web.Controllers;

[Route("[controller]")]
public class BasketsController : Controller
{
    private readonly IBasketService _basketService;

    public BasketsController(IBasketService basketService)
    {
        _basketService = basketService;
    }

    public async Task<IActionResult> Index()
    {
        var basket = await _basketService.GetBasketAsync();
        return View(basket);
    }

    [HttpGet("add-item")]
    public async Task<IActionResult> AddBasketItem(BasketItemViewModel item)
    {
        var result = await _basketService.AddItemToBasketAsync(item);
        return RedirectToAction(nameof(Index));
    }

    [HttpGet("remove-item")]
    public async Task<IActionResult> RemoveBasketItem(string itemId)
    {
        var result = await _basketService.RemoveItemFromBasketAsync(itemId);
        return RedirectToAction(nameof(Index));
    }

    [HttpGet("apply-discount")]
    public async Task<IActionResult> ApplyDiscount(string discountCode)
    {
        var result = await _basketService.ApplyDiscountAsync(discountCode);
        if(!result) TempData["discountStatus"] = "İndirim kodu geçersiz";
        return RedirectToAction(nameof(Index));
    }

    [HttpGet("cancel-discount")]
    public async Task<IActionResult> CancelDiscount(string discountCode)
    {
        var result = await _basketService.CancelDiscountAsync(discountCode);
        return RedirectToAction(nameof(Index));
    }

    [HttpGet("clear-basket")]
    public async Task<IActionResult> ClearBasket()
    {
        await _basketService.ClearBasketAsync();
        return RedirectToAction(nameof(Index));
    }
}