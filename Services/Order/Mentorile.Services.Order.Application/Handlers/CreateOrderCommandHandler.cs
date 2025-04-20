using MediatR;
using Mentorile.Services.Order.Application.Commands;
using Mentorile.Services.Order.Application.DTOs;
using Mentorile.Services.Order.Domain.OrderAggreagate;
using Mentorile.Services.Order.Infrastructure.Contexts;
using Mentorile.Shared.Common;

namespace Mentorile.Services.Order.Application.Handlers;
public class CreateOrderCommandHandler : IRequestHandler<CreateOrderCommand, Result<CreatedOrderDTO>>
{
    private readonly OrderDbContext _context;

    public CreateOrderCommandHandler(OrderDbContext context)
    {
        _context = context;
    }

    public async Task<Result<CreatedOrderDTO>> Handle(CreateOrderCommand request, CancellationToken cancellationToken)
    {
        var newAddress = new Address(request.Address.Province, request.Address.District, request.Address.Street, request.Address.Line, request.Address.ZipCode);
        Domain.OrderAggreagate.Order newOrder = new Domain.OrderAggreagate.Order(request.BuyerId, newAddress);

        request.OrderItems.ForEach(x =>{
            newOrder.AddOrderItem(x.ItemId, x.ItemName, x.Price, x.PictureUri);
        });

        await _context.Orders.AddAsync(newOrder);
        await _context.SaveChangesAsync();
        var createdOrderDTO = new CreatedOrderDTO() { OrderId = newOrder.Id}; 
        return Result<CreatedOrderDTO>.Success(createdOrderDTO);
    }
}