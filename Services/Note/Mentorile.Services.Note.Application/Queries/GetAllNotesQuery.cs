using MediatR;
using Mentorile.Services.Note.Application.DTOs;
using Mentorile.Shared.Common;

namespace Mentorile.Services.Note.Application.Queries;
public class GetAllNotesQuery : IRequest<Result<PagedResult<NoteDTO>>>
{
    public PagingParams PagingParams { get; set; }
    public GetAllNotesQuery(PagingParams? pagingParams)
    {
        PagingParams = pagingParams ?? new PagingParams();
    }
}