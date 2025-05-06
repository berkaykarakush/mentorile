using Mentorile.Client.Web.Services.Abstracts;
using Mentorile.Client.Web.ViewModels.Discounts;
using Mentorile.Shared.Common;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Mentorile.Client.Web.Areas.Admin.Controllers;

[Authorize]
[Area("Admin")]
[Route("Admin/[controller]")]
public class DiscountsController : Controller
{
    private readonly IDiscountService _discountService;
    private readonly ILogger<DiscountsController> _logger;

    public DiscountsController(ILogger<DiscountsController> logger, IDiscountService discountService)
    {
        _logger = logger;
        _discountService = discountService;
    }

    [HttpGet]
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
}