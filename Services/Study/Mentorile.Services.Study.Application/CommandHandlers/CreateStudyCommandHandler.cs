using AutoMapper;
using MediatR;
using Mentorile.Services.Study.Application.Commands;
using Mentorile.Services.Study.Application.Responses;
using Mentorile.Services.Study.Domain.Services;
using Mentorile.Shared.Common;

namespace Mentorile.Services.Study.Application.CommandHandlers;
public class CreateStudyCommandHandler : IRequestHandler<CreateStudyCommand, Result<CreateStudyResponse>>
{
    private readonly IStudyService _studyService;
    private readonly IMapper _mapper;

    public CreateStudyCommandHandler(IStudyService studyService, IMapper mapper)
    {
        _studyService = studyService;
        _mapper = mapper;
    }

    public async Task<Result<CreateStudyResponse>> Handle(CreateStudyCommand request, CancellationToken cancellationToken)
    {
        var study = _mapper.Map<Domain.Entities.Study>(request);
        study.CreatedDate = DateTime.UtcNow;
        var result = await _studyService.CreateStudyAsync(study);
        return result.IsSuccess
            ? Result<CreateStudyResponse>.Success(new CreateStudyResponse{ Succeeded = true, Message = result.Message, StudyId = study.Id })
            : Result<CreateStudyResponse>.Failure(result.Message);
    }
}