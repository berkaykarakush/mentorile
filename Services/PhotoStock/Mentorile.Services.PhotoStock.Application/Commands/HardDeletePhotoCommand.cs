using MediatR;
using Mentorile.Shared.Common;

namespace Mentorile.Services.PhotoStock.Application.Commands;
public class HardDeletePhotoCommand : IRequest<Result<bool>>
{
    public string PhotoId { get; set; }    
}