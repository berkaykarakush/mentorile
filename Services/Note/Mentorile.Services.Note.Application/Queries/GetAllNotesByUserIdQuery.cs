using MediatR;
using Mentorile.Services.Note.Application.DTOs;
using Mentorile.Shared.Common;

namespace Mentorile.Services.Note.Application.Queries;

public class GetAllNotesByUserIdQuery : IRequest<Result<PagedResult<NoteDTO>>>
{
    public PagingParams PagingParams { get; set; }
    public string UserId { get; set; }
    public GetAllNotesByUserIdQuery(PagingParams? pagingParams, string userId)
    {
        PagingParams = pagingParams ?? new PagingParams();
        UserId = userId;
    }
}