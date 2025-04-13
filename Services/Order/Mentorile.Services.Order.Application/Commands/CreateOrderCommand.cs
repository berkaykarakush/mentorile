using MediatR;
using Mentorile.Services.Order.Application.DTOs;
using Mentorile.Shared.Common;

namespace Mentorile.Services.Order.Application.Commands;
public class CreateOrderCommand : IRequest<Result<CreatedOrderDTO>>
{
    public string BuyerId { get; set; }
    public List<OrderItemDTO> OrderItems { get; set; }
    public AddressDTO Address { get; set; }
}