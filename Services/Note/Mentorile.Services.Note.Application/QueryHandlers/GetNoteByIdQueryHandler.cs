using AutoMapper;
using MediatR;
using Mentorile.Services.Note.Application.DTOs;
using Mentorile.Services.Note.Application.Queries;
using Mentorile.Services.Note.Domain.Interfaces;
using Mentorile.Shared.Common;

namespace Mentorile.Services.Note.Application.QueryHandlers;
public class GetNoteByIdQueryHandler : IRequestHandler<GetNoteByIdQuery, Result<NoteDTO>>
{
    private readonly INoteRepository _noteRepository;
    private readonly IMapper _mapper;

    public GetNoteByIdQueryHandler(INoteRepository noteRepository, IMapper mapper)
    {
        _noteRepository = noteRepository;
        _mapper = mapper;
    }

    public async Task<Result<NoteDTO>> Handle(GetNoteByIdQuery request, CancellationToken cancellationToken)
    {
        var noteResult = await _noteRepository.GetNoteByIdAsync(request.NoteId, cancellationToken);
        if (!noteResult.IsSuccess || noteResult.Data == null)
            return Result<NoteDTO>.Failure("Note not found.");

        var dto = _mapper.Map<NoteDTO>(noteResult.Data);
        return Result<NoteDTO>.Success(dto, "Note founded.");
    }
}