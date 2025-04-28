using MediatR;
using Mentorile.Services.Study.Application.Responses;
using Mentorile.Shared.Common;

namespace Mentorile.Services.Study.Application.Commands;
public class UpdateStudyCommand : IRequest<Result<UpdateStudyResponse>>
{
    public string Id { get; set; }
    public string Name { get; set; }
    public string UserId { get; set; }    
}