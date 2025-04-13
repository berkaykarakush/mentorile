using MediatR;
using Mentorile.Services.Order.Application.DTOs;
using Mentorile.Shared.Common;

namespace Mentorile.Services.Order.Application.Queries;
public class GetOrdersByUserIdQuery : IRequest<Result<List<OrderDTO>>>
{
    public string UserId { get; set; }
}