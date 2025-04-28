using MediatR;
using Mentorile.Services.Study.Application.Responses;
using Mentorile.Shared.Common;

namespace Mentorile.Services.Study.Application.Commands;
public class CreateStudyCommand : IRequest<Result<CreateStudyResponse>>
{
    public string Name { get; set; }
    public string UserId { get; set; }
}