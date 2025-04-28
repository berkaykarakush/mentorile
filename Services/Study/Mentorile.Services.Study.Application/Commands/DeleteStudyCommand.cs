using MediatR;
using Mentorile.Services.Study.Application.Responses;
using Mentorile.Shared.Common;

namespace Mentorile.Services.Study.Application.Commands;
public class DeleteStudyCommand : IRequest<Result<DeleteStudyResponse>>
{
    public string Id { get; set; }        
}