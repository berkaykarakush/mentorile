using Mentorile.Client.Web.ViewModels.Notes;
using Mentorile.Shared.Common;

namespace Mentorile.Client.Web.Services.Abstracts;
public interface INoteService
{
    Task<PagedResult<NoteViewModel>> GetAllAsync(PagingParams pagingParams, CancellationToken cancellationToken = default);
    Task<PagedResult<NoteViewModel>> GetAllByUserIdAsync(PagingParams pagingParams, CancellationToken cancellationToken = default);
    Task<NoteViewModel> GetByIdAsync(Guid noteId, CancellationToken cancellationToken = default);
    Task<Result<bool>> CreateAsync(CreateNoteInput input, CancellationToken cancellationToken = default);
    Task<Result<bool>> UpdateAsync(UpdateNoteInput input, CancellationToken cancellationToken = default);
    Task<Result<bool>> DeleteAsync(Guid noteId, CancellationToken cancellationToken= default);
}