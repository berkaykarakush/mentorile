using AutoMapper.Internal.Mappers;
using MediatR;
using Mentorile.Services.Order.Application.DTOs;
using Mentorile.Services.Order.Application.Mapping;
using Mentorile.Services.Order.Application.Queries;
using Mentorile.Services.Order.Infrastructure.Contexts;
using Mentorile.Shared.Common;
using Microsoft.EntityFrameworkCore;

namespace Mentorile.Services.Order.Application.Handlers;
public class GetOrdersByUserIdQueryHandler : IRequestHandler<GetOrdersByUserIdQuery, Result<List<OrderDTO>>>
{
    private readonly OrderDbContext _context;

    public GetOrdersByUserIdQueryHandler(OrderDbContext context)
    {
        _context = context;
    }

    public async Task<Result<List<OrderDTO>>> Handle(GetOrdersByUserIdQuery request, CancellationToken cancellationToken)
    {
        var orders = await _context.Orders.Include(x => x.OrderItems).Where(x => x.BuyerId == request.UserId).ToListAsync();
        if(!orders.Any()) return Result<List<OrderDTO>>.Success(new List<OrderDTO>());

        var ordersDTO = ObjectMapper.Mapper.Map<List<OrderDTO>>(orders);
        return Result<List<OrderDTO>>.Success(ordersDTO);
    }
}