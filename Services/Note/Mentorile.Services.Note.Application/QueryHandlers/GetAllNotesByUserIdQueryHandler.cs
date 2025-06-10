using AutoMapper;
using MediatR;
using Mentorile.Services.Note.Application.DTOs;
using Mentorile.Services.Note.Application.Queries;
using Mentorile.Services.Note.Domain.Interfaces;
using Mentorile.Shared.Common;

namespace Mentorile.Services.Note.Application.QueryHandlers;
public class GetAllNotesByUserIdQueryHandler : IRequestHandler<GetAllNotesByUserIdQuery, Result<PagedResult<NoteDTO>>>
{
    private readonly INoteRepository _noteRepository;
    private readonly IMapper _mapper;

    public GetAllNotesByUserIdQueryHandler(INoteRepository noteRepository, IMapper mapper)
    {
        _noteRepository = noteRepository;
        _mapper = mapper;
    }

    public async Task<Result<PagedResult<NoteDTO>>> Handle(GetAllNotesByUserIdQuery request, CancellationToken cancellationToken)
    {
        var pagingParams = request.PagingParams ?? new PagingParams();
        var notesResult = await _noteRepository.GetAllNotesByUserIdAsync(request.UserId, pagingParams, cancellationToken);
        if (!notesResult.IsSuccess || notesResult.Data.Data == null)
            return Result<PagedResult<NoteDTO>>.Failure("No notes found.");

        var dtos = _mapper.Map<List<NoteDTO>>(notesResult.Data.Data);

        var paged = PagedResult<NoteDTO>.Create(dtos, notesResult.Data.TotalCount, pagingParams);
        return Result<PagedResult<NoteDTO>>.Success(paged, "Notes founded.");
    }
}