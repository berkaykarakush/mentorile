using MediatR;
using Mentorile.Shared.Common;

namespace Mentorile.Services.Discount.Application.Commands;
public class DeleteDiscountCommand : IRequest<Result<bool>>
{
    public string DiscountId { get; set; } = string.Empty;
}