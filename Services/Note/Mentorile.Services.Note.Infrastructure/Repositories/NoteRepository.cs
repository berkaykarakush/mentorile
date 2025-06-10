using Mentorile.Services.Note.Domain.Exceptions;
using Mentorile.Services.Note.Domain.Interfaces;
using Mentorile.Services.Note.Infrastructure.Persistence;
using Mentorile.Shared.Common;
using Mentorile.Shared.Services.Abstracts;
using Microsoft.EntityFrameworkCore;
namespace Mentorile.Services.Note.Infrastructure.Repositories;

public class NoteRepository : INoteRepository
{
    private readonly AppDbContext _appDbContext;
    private readonly IExecutor _executor;

    public NoteRepository(AppDbContext appDbContext, IExecutor executor)
    {
        _appDbContext = appDbContext;
        _executor = executor;
    }

    public async Task<Result<bool>> CreateAsync(Domain.Entities.Note note, CancellationToken cancellationToken = default)
        => await _executor.ExecuteAsync(async () => {
            await _appDbContext.Notes.AddAsync(note);
            var result = await _appDbContext.SaveChangesAsync(cancellationToken) > 0;
            if (!result) throw new NoteCreateException();
            return result;
        });

    public async Task<Result<bool>> DeleteAsync(Guid noteId, CancellationToken cancellationToken = default)
        => await _executor.ExecuteAsync(async () => {
            var note = await _appDbContext.Notes.FirstOrDefaultAsync(n => n.Id == noteId, cancellationToken);
            if (note == null) throw new NoteNotFoundException();

            note.Delete();
            _appDbContext.Notes.Update(note);

            var result = await _appDbContext.SaveChangesAsync(cancellationToken) > 0;
            if (!result) throw new NoteDeleteException();

            return result;
        });

    public async Task<Result<bool>> UpdateAsync(Domain.Entities.Note note, CancellationToken cancellationToken = default)
        => await _executor.ExecuteAsync(async () =>
        {
            _appDbContext.Notes.Update(note);
            var result = await _appDbContext.SaveChangesAsync(cancellationToken) > 0;
            if (!result) throw new NoteUpdateException();

            return result;
        });

    public async Task<Result<PagedResult<Domain.Entities.Note>>> GetAllNotesAsync(PagingParams pagingParams, CancellationToken cancellationToken = default)
        => await _executor.ExecuteAsync(async () =>
        {
            var query = _appDbContext.Notes.AsQueryable();
            var totalCount = await query.CountAsync(cancellationToken);
            var notes = await query
                .Skip((pagingParams.PageNumber - 1) * pagingParams.PageSize)
                .Take(pagingParams.PageSize)
                .ToListAsync(cancellationToken);
            var paged = PagedResult<Domain.Entities.Note>.Create(notes, totalCount, pagingParams);
            return paged;
        });

    public async Task<Result<PagedResult<Domain.Entities.Note>>> GetAllNotesByUserIdAsync(string userId, PagingParams pagingParams, CancellationToken cancellationToken = default)
        => await _executor.ExecuteAsync(async () =>
        {
            var query = _appDbContext.Notes.Where(q => q.UserId == userId);
            var totalCount = await query.CountAsync(cancellationToken);
            var notes = await query
                .Skip((pagingParams.PageNumber - 1) * pagingParams.PageSize)
                .Take(pagingParams.PageSize)
                .ToListAsync(cancellationToken);
            var paged = PagedResult<Domain.Entities.Note>.Create(notes, totalCount, pagingParams);
            return paged;
        });

    public async Task<Result<Domain.Entities.Note>> GetNoteByIdAsync(Guid noteId, CancellationToken cancellationToken = default)
        => await _executor.ExecuteAsync(async () =>
        {
            var note = await _appDbContext.Notes.FirstOrDefaultAsync(n => n.Id == noteId, cancellationToken);
            if (note == null) throw new NoteNotFoundException();

            return note;
        });
}