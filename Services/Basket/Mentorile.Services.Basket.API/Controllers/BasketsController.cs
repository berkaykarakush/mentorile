using MediatR;
using Mentorile.Services.Basket.Application.Command;
using Mentorile.Services.Basket.Application.Queries;
using Mentorile.Shared.ControllerBases;
using Microsoft.AspNetCore.Mvc;

namespace Mentorile.Services.Basket.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class BasketsController : CustomControllerBase
{
    private readonly IMediator _mediator;

    public BasketsController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpPost("add-item")]
    public async Task<IActionResult> AddItemToBasket(AddItemToBasketCommand command)
        => CreateActionResultInstance(await _mediator.Send(command));

    [HttpDelete("delete-item/{itemId}")]
    public async Task<IActionResult> RemoveItemFromBasket(string itemId)
        => CreateActionResultInstance(await _mediator.Send(new RemoveItemFromBasketCommand { ItemId = itemId }));

    [HttpGet]
    public async Task<IActionResult> GetBasket()
        => CreateActionResultInstance(await _mediator.Send(new GetBasketQuery()));

    [HttpPost("apply-discount/{discountCode}")]
    public async Task<IActionResult> ApplyDiscount(string discountCode)
        => CreateActionResultInstance(await _mediator.Send(new ApplyDiscountCommand { DiscountCode = discountCode }));

    [HttpPost("cancel-discount/{discountCode}")]
    public async Task<IActionResult> CancelDiscount(string discountCode)
        => CreateActionResultInstance(await _mediator.Send(new CancelDiscountCommand { DiscountCode = discountCode }));

    [HttpDelete("clear-basket")]
    public async Task<IActionResult> ClearBasket()
        => CreateActionResultInstance(await _mediator.Send(new ClearBasketCommand())); 
}