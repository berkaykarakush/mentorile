using MediatR;
using Mentorile.Shared.Common;

namespace Mentorile.Services.PhotoStock.Application.Commands;
public class DeletePhotoCommand : IRequest<Result<bool>>
{
    public string PhotoId { get; set; }
}