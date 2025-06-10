using Mentorile.Shared.Common;

namespace Mentorile.Services.Note.Domain.Interfaces;
public interface INoteRepository
{
    Task<Result<PagedResult<Entities.Note>>> GetAllNotesAsync(PagingParams pagingParams, CancellationToken cancellationToken= default);
    Task<Result<PagedResult<Entities.Note>>> GetAllNotesByUserIdAsync(string userId, PagingParams pagingParams, CancellationToken cancellationToken = default);
    Task<Result<Entities.Note>> GetNoteByIdAsync(Guid noteId, CancellationToken cancellationToken= default);
    Task<Result<bool>> CreateAsync(Entities.Note note, CancellationToken cancellationToken= default);
    Task<Result<bool>> UpdateAsync(Entities.Note note, CancellationToken cancellationToken= default);
    Task<Result<bool>> DeleteAsync(Guid noteId, CancellationToken cancellationToken= default);

}