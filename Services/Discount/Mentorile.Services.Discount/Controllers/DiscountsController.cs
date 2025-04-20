using Mentorile.Services.Discount.DTOs;
using Mentorile.Services.Discount.Services;
using Mentorile.Shared.ControllerBases;
using Microsoft.AspNetCore.Mvc;

namespace Mentorile.Services.Discount.Controllers;
[ApiController]
[Route("api/[controller]")]
public class DiscountsController : CustomControllerBase
{
    private readonly IDiscountService _discountService;

    public DiscountsController(IDiscountService discountService)
    {
        _discountService = discountService;
    }

    [HttpGet]
    public async Task<IActionResult> GetAll(){
        var result = await _discountService.GetAllAsync();
        return CreateActionResultInstance(result);
    }

    [HttpGet("{code}/{userId}")]
    public async Task<IActionResult> GetByCode(string code, string userId)
    {
        var result = await _discountService.GetByCodeAndUserIdAsync(code, userId);
        return CreateActionResultInstance(result);
    }

    [HttpPost("create")]
    public async Task<IActionResult> Create([FromBody] CreateDiscountDTO createDiscountDTO)
    {
        var result = await _discountService.CreateAsync(createDiscountDTO);
        return CreateActionResultInstance(result);
    }

    [HttpDelete("{discountId}")]
    public async Task<IActionResult> Delete(string discountId)
    {
        var result = await _discountService.DeleteAsync(discountId);
        return CreateActionResultInstance(result);
    }

    [HttpPost("apply")]
    public async Task<IActionResult> ApplyDiscount([FromBody] ApplyDiscountDTO applyDiscountDTO)
    {
        var result = await _discountService.ApplyDiscountAsync(applyDiscountDTO.Code, applyDiscountDTO.TotalPrice);
        return CreateActionResultInstance(result);
    }

    [HttpPost("cancel/{code}")]
    public async Task<IActionResult> CancelApplyDiscount(string code)
    {
        var result = await _discountService.CancelDiscountAsync(code);
        return CreateActionResultInstance(result);
    }
    
}