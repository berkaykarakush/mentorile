using AutoMapper;
using MediatR;
using Mentorile.Services.Study.Application.Commands;
using Mentorile.Services.Study.Application.Responses;
using Mentorile.Services.Study.Domain.Services;
using Mentorile.Shared.Common;

namespace Mentorile.Services.Study.Application.CommandHandlers;
public class DeleteStudyCommandHandler : IRequestHandler<DeleteStudyCommand, Result<DeleteStudyResponse>>
{
    private readonly IStudyService _studyService;

    public DeleteStudyCommandHandler(IStudyService studyService)
    {
        _studyService = studyService;
    }

    public async Task<Result<DeleteStudyResponse>> Handle(DeleteStudyCommand request, CancellationToken cancellationToken)
    {
        var result = await _studyService.DeleteStudyAsync(request.Id);

        return result.IsSuccess 
            ? Result<DeleteStudyResponse>.Success(new DeleteStudyResponse{ Succeeded = true, Message = result.Message })
            : Result<DeleteStudyResponse>.Failure(result.Message); 
    }
}