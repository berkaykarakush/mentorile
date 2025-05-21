using MediatR;
using Mentorile.Shared.Common;

namespace Mentorile.Services.Basket.Application.Command;
public class RemoveItemFromBasketCommand : IRequest<Result<bool>>
{
    public string ItemId { get; set; }
}