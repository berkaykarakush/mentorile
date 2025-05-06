using Mentorile.Client.Web.Services.Abstracts;
using Mentorile.Client.Web.ViewModels.Discounts;
using Mentorile.Shared.Common;
using Mentorile.Shared.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Mentorile.Client.WebControllers;
[Route("[controller]")]

public class DiscountsController : Controller
{
    private readonly ILogger<DiscountsController> _logger;
    private readonly IDiscountService _discountService;
    private readonly ISharedIdentityService _sharedIdentityService;
    private readonly IUserService _userService;
    public DiscountsController(ILogger<DiscountsController> logger, IDiscountService discountService, ISharedIdentityService sharedIdentityService, IUserService userService)
    {
        _logger = logger;
        _discountService = discountService;
        _sharedIdentityService = sharedIdentityService;
        _userService = userService;
    }

    [HttpGet("all-discount-codes")]
    public async Task<IActionResult> Index(PagingParams pagingParams)
    {
        var discounts = await _discountService.GetAllDiscountAsync();
        ViewBag.Paging = pagingParams;
        if(discounts == null)
        {
            var emptyResult = new PagedResult<DiscountViewModel>
            (
                new List<DiscountViewModel>(),
                0,
                pagingParams,
                200,
                "No data available"
            );
            return View(emptyResult);
        }

        var totalCount = discounts.TotalCount;
        var statusCode = 200;
        var message = "Success";
        var listDiscounts = new List<DiscountViewModel>();
        foreach (var item in discounts.Data)
            listDiscounts.Add(new DiscountViewModel()
            {
                Id = item.Id,
                UserIds = item.UserIds,
                Rate = item.Rate,
                Code = item.Code,
                IsActive = item.IsActive,
                ExpirationDate = item.ExpirationDate,
                CreateAt = item.CreateAt,
            });

        var pagedResult = new PagedResult<DiscountViewModel>
        ( 
            listDiscounts,
            totalCount,
            pagingParams,
            statusCode,
            message
        );

        return View(pagedResult);
    }

    [HttpGet("user-discount-codes")]
    public async Task<IActionResult> UserDiscountCodes(PagingParams pagingParams)
    {
        var discounts = await _discountService.GetAllDiscountByUserIdAsync(_sharedIdentityService.GetUserId);
        ViewBag.Paging = pagingParams;
        if(discounts == null)
        {
            var emptyResult = new PagedResult<DiscountViewModel>
            (
                new List<DiscountViewModel>(),
                0,
                pagingParams,
                200,
                "No data available"
            );
            return View(emptyResult);
        }

        var totalCount = discounts.TotalCount;
        var statusCode = 200;
        var message = "Success";
        var listDiscounts = new List<DiscountViewModel>();
        foreach (var item in discounts.Data)
            listDiscounts.Add(new DiscountViewModel()
            {
                Id = item.Id,
                UserIds = item.UserIds,
                Rate = item.Rate,
                Code = item.Code,
                IsActive = item.IsActive,
                ExpirationDate = item.ExpirationDate,
                CreateAt = item.CreateAt,
            });

        var pagedResult = new PagedResult<DiscountViewModel>
        ( 
            listDiscounts,
            totalCount,
            pagingParams,
            statusCode,
            message
        );

        return View(pagedResult);
    } 

    [HttpGet("create")]
    public async Task<IActionResult> Create()
    {
        var users = await _userService.GetAllUsers();    
        var userSelectList = new SelectList(users, "Id", "UserName");
        ViewBag.UserSelectList = userSelectList;
        return View();
    }

    [HttpPost("create")]
    public async Task<IActionResult> Create(CreateDiscountInput createDiscountInput)
    {
        if(!ModelState.IsValid) return View(createDiscountInput);
        await _discountService.CreateDiscountAsnyc(createDiscountInput);
        return RedirectToAction(nameof(Index));
    }

    [HttpGet("update")]
    public async Task<IActionResult> Update(string discountId)
    {
        
        var discount = await _discountService.GetDiscountByIdAsync(discountId);
        if(discount == null) return RedirectToAction(nameof(Index));

        var updateDiscountInput = new UpdateDiscountInput(){
            Id = discount.Id,
            UserIds = discount.UserIds,
            Rate = discount.Rate,
            Code = discount.Code,
            IsActive = discount.IsActive,
            ExpirationDate = discount.ExpirationDate
        };
        return View(updateDiscountInput);
    }

    [HttpPost("update")]
    public async Task<IActionResult> Update(UpdateDiscountInput updateDiscountInput)
    {
        if(!ModelState.IsValid) return View(updateDiscountInput);

        var existingDiscount = await _discountService.GetDiscountByIdAsync(updateDiscountInput.Id);
        if(existingDiscount == null) return RedirectToAction(nameof(Index));
        await _discountService.UpdateDiscountAsnyc(updateDiscountInput);
        return RedirectToAction(nameof(Update), updateDiscountInput.Id);
    }

    [HttpGet("delete")]
    public async Task<IActionResult> Delete(string discountId)
    {
        await _discountService.DeleteDiscountAsnyc(discountId);
        return RedirectToAction(nameof(Index));
    }
}