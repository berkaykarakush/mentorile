using Mentorile.Services.Basket.DTOs;
using Mentorile.Services.Basket.Services;
using Mentorile.Shared.ControllerBases;
using Microsoft.AspNetCore.Mvc;

namespace Mentorile.Services.Basket.Controllers;

[ApiController]
[Route("api/[controller]")]
public class BasketsController : CustomControllerBase
{
    private readonly IBasketService _basketService;

    public BasketsController(IBasketService basketService)
    {
        _basketService = basketService;
    }

    [HttpPost("add-item")]
    public async Task<IActionResult> AddItemToBasket(BasketItemDTO basketItemDTO)
    {
        var result = await _basketService.AddItemToBasketAsync(basketItemDTO);
        return CreateActionResultInstance(result);
    }

    [HttpDelete("delete-item")]
    public async Task<IActionResult> RemoveItemFromBasket(string itemId)
    {
        var result = await _basketService.RemoveItemFromBasketAsync(itemId);
        return CreateActionResultInstance(result);
    }

    [HttpGet("get-basket")]
    public async Task<IActionResult> GetBasket()
    {
        var result = await _basketService.GetBasketAsync();
        return CreateActionResultInstance(result);
    }

    [HttpDelete("clear-basket")]
    public async Task<IActionResult> ClearBasket()
    {
        var result = await _basketService.ClearBasketAsync();
        return CreateActionResultInstance(result);
    }
}