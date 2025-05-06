using MediatR;
using Mentorile.Services.Discount.Application.Commands;
using Mentorile.Services.Discount.Application.Queries;
using Mentorile.Shared.Common;
using Mentorile.Shared.ControllerBases;
using Microsoft.AspNetCore.Mvc;

namespace Mentorile.Services.Discount.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class DiscountsController : CustomControllerBase
{
    private readonly IMediator _mediator;

    public DiscountsController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet]
    public async Task<IActionResult> GetAll([FromQuery] PagingParams pagingParams)
        => CreateActionResultInstance(await _mediator.Send(new GetAllDiscountsQuery { PagingParams = pagingParams}));

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(string id)
        => CreateActionResultInstance(await _mediator.Send(new GetDiscountByIdQuery {DiscountId = id}));

    [HttpGet("by-user/{userId}")]
    public async Task<IActionResult> GetAllByUserId(string userId, [FromQuery] PagingParams pagingParams)
        => CreateActionResultInstance(await _mediator.Send(new GetAllDiscountsByUserIdQuery { UserId = userId, PagingParams = pagingParams}));

    [HttpGet("{code}/for-user/{userId}")]
    public async Task<IActionResult> GetByCode(string code, string userId)
        => CreateActionResultInstance(await _mediator.Send(new GetDiscountByCodeAndUserIdQuery { Code = code, UserId = userId }));

    
    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CreateDiscountCommand command)
        => CreateActionResultInstance(await _mediator.Send(command));

    [HttpPut("{id}")]
    public async Task<IActionResult> Update(string id, [FromBody] UpdateDiscountCommand command)
    {
        command.Discount.Id = id;
        return CreateActionResultInstance(await _mediator.Send(command));
    }
    
    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(string id)
        => CreateActionResultInstance(await _mediator.Send(new DeleteDiscountCommand { DiscountId = id }));

    [HttpPost("apply/{code}")]
    public async Task<IActionResult> ApplyDiscount(string code, [FromBody] ApplyDiscountCommand command)
    {
        command.Code = code;
        return CreateActionResultInstance(await _mediator.Send(command));
    }

    [HttpPost("cancel/{code}")]
    public async Task<IActionResult> CancelDiscount(string code, [FromBody] CancelDiscountCommand command)
    {
        command.Code = code;
        return CreateActionResultInstance(await _mediator.Send(command));
    }
}