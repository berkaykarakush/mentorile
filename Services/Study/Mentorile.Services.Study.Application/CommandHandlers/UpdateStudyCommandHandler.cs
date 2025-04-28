using AutoMapper;
using MediatR;
using Mentorile.Services.Study.Application.Commands;
using Mentorile.Services.Study.Application.Responses;
using Mentorile.Services.Study.Domain.Services;
using Mentorile.Shared.Common;

namespace Mentorile.Services.Study.Application.CommandHandlers;
public class UpdateStudyCommandHandler : IRequestHandler<UpdateStudyCommand, Result<UpdateStudyResponse>>
{
    private readonly IStudyService _studyService;
    private readonly IMapper _mapper;

    public UpdateStudyCommandHandler(IStudyService studyService, IMapper mapper)
    {
        _studyService = studyService;
        _mapper = mapper;
    }

    public async Task<Result<UpdateStudyResponse>> Handle(UpdateStudyCommand request, CancellationToken cancellationToken)
    {
        var study = _mapper.Map<Domain.Entities.Study>(request);
        var result = await _studyService.UpdateStudyAsync(study);
        return result.IsSuccess 
            ? Result<UpdateStudyResponse>.Success(new UpdateStudyResponse{ Message = result.Message, Succeeded = result.IsSuccess })
            : Result<UpdateStudyResponse>.Failure(result.Message);
    }
}