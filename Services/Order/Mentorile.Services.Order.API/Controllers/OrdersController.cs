using MediatR;
using Mentorile.Services.Order.Application.Commands;
using Mentorile.Services.Order.Application.Queries;
using Mentorile.Shared.ControllerBases;
using Mentorile.Shared.Services;
using Microsoft.AspNetCore.Mvc;

namespace Mentorile.Services.Order.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class OrdersController : CustomControllerBase
{
    private readonly IMediator _mediator;
    private readonly ISharedIdentityService _sharedIdentityService;

    public OrdersController(IMediator mediator, ISharedIdentityService sharedIdentityService)
    {
        _mediator = mediator;
        _sharedIdentityService = sharedIdentityService;
    }

    [HttpGet]
    public async Task<IActionResult> GetOrders()
    {
        var response = await _mediator.Send(new GetOrdersByUserIdQuery{ UserId = _sharedIdentityService.GetUserId });
        return CreateActionResultInstance(response);
    }

    [HttpPost]
    public async Task<IActionResult> CreateOrder(CreateOrderCommand createOrderCommand)
    {
        var response = await _mediator.Send(createOrderCommand);
        return CreateActionResultInstance(response);
    }

    

}