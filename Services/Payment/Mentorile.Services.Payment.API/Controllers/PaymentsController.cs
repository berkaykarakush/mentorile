using Mentorile.Services.Payment.Application.Commands;
using Mentorile.Services.Payment.Application.Queries;
using Mentorile.Services.Payment.Domain.Enums;
using Mentorile.Shared.Common;
using Mentorile.Shared.ControllerBases;
using Microsoft.AspNetCore.Mvc;

namespace Mentorile.Services.Payment.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class PaymentsController : CustomControllerBase
{
   private readonly MediatR.IMediator _mediator;

    public PaymentsController(MediatR.IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CreatePaymentCommand command)
        => CreateActionResultInstance(await _mediator.Send(command));

    [HttpPost("receive")]
    public async Task<IActionResult> Receive([FromBody] ReceivePaymentCommand command)
        // => CreateActionResultInstance(await _mediator.Send(command));
    {
            if (!ModelState.IsValid)
        {
            var errors = ModelState.Values.SelectMany(x => x.Errors).Select(x => x.ErrorMessage);
            System.Console.WriteLine(errors);
            return BadRequest(errors);
        }

        var result = await _mediator.Send(command);
        return result.IsSuccess ? Ok(result) : BadRequest(result);
    }

    [HttpPut]
    public async Task<IActionResult> Update([FromBody] UpdatePaymentCommand command)
        => CreateActionResultInstance(await _mediator.Send(command));

    [HttpGet]
    public async Task<IActionResult> GetAll([FromQuery] PagingParams pagingParams)
        => CreateActionResultInstance(await _mediator.Send(new GetAllPaymentsQuery {PagingParams = pagingParams}));
    
    [HttpGet("by-user/{userId}")]
    public async Task<IActionResult> GetAllByOrderId(string userId, [FromQuery] PagingParams pagingParams)
        => CreateActionResultInstance(await _mediator.Send(new GetAllPaymentByUserIdQuery { UserId = userId, PagingParams = pagingParams}));

    [HttpGet("by-status/{paymentStatus}")]
    public async Task<IActionResult> GetAllByOrderId(PaymentStatus paymentStatus, [FromQuery] PagingParams pagingParams)
        => CreateActionResultInstance(await _mediator.Send(new GetAllPaymentsByStatusQuery { PaymentStatus = paymentStatus, PagingParams = pagingParams}));

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(string id)
        => CreateActionResultInstance(await _mediator.Send(new GetPaymentByIdQuery { Id = id }));

    [HttpGet("by-order/{orderId}")]
    public async Task<IActionResult> GetByOrderId(string orderId)
        => CreateActionResultInstance(await _mediator.Send(new GetPaymentByOrderIdQuery { OrderId = orderId}));

    [HttpGet("by-transaction/{transactionId}")]
    public async Task<IActionResult> GetByTransactionId(string transactionId)
        => CreateActionResultInstance(await _mediator.Send(new GetPaymentByTransactionIdQuery { TransactionId = transactionId}));

    
}