using Mentorile.Services.Study.Domain.Services;
using Mentorile.Services.Study.Infrastructure.Persistence;
using Mentorile.Shared.Common;
using Microsoft.EntityFrameworkCore;

namespace Mentorile.Services.Study.Infrastructure.Repositories;
public class StudyRepository : IStudyService
{
    private readonly AppDbContext _appDbContext;

    public StudyRepository(AppDbContext appDbContext)
    {
        _appDbContext = appDbContext;
    }

    public async Task<Result<Domain.Entities.Study>> CreateStudyAsync(Domain.Entities.Study study)
    {
        _appDbContext.Studies.Add(study);
        await _appDbContext.SaveChangesAsync();
        return Result<Domain.Entities.Study>.Success(study);
    }

    public async Task<Result<Domain.Entities.Study>> UpdateStudyAsync(Domain.Entities.Study study)
    {
        _appDbContext.Studies.Update(study);
        await _appDbContext.SaveChangesAsync();
        return Result<Domain.Entities.Study>.Success(study);
    }
    public async Task<Result<Domain.Entities.Study>> DeleteStudyAsync(string id)
    {
        var study = await _appDbContext.Studies.FirstOrDefaultAsync(s => s.Id == id);
        if(study == null) return Result<Domain.Entities.Study>.Failure("study is null");
        _appDbContext.Studies.Remove(study);
        await _appDbContext.SaveChangesAsync();
        return Result<Domain.Entities.Study>.Success(study);
    }

    public async Task<Result<PagedResult<Domain.Entities.Study>>> GetAllAsync(PagingParams pagingParams)
    {
        // pagination
        var query = _appDbContext.Studies.AsQueryable();
        // get total count
        var totalCount = await query.CountAsync();
        
        var studies = await query
            .Skip((pagingParams.PageNumber - 1) * pagingParams.PageSize)
            .Take(pagingParams.PageSize)
            .ToListAsync();

        var pagedResult = PagedResult<Domain.Entities.Study>.Create(studies, totalCount, pagingParams);
        return Result<PagedResult<Domain.Entities.Study>>.Success(pagedResult);
    }

    public async Task<Result<PagedResult<Domain.Entities.Study>>> GetAllByUserIdAsync(string userId, PagingParams pagingParams)
    {
        var query = _appDbContext.Studies.AsQueryable();
        var totalCount = await query.CountAsync();
        var studies = await query
            .Where(s => s.UserId == userId)
            .Skip((pagingParams.PageNumber - 1) * pagingParams.PageSize)
            .Take(pagingParams.PageSize)
            .ToListAsync();
        var pagedResult = PagedResult<Domain.Entities.Study>.Create(studies, totalCount, pagingParams);
        return Result<PagedResult<Domain.Entities.Study>>.Success(pagedResult);
        
    }

    public async Task<Result<Domain.Entities.Study>> GetByIdAsync(string id)
    {
        var study = await _appDbContext.Studies.FirstOrDefaultAsync(s => s.Id == id);
        if(study == null) return Result<Domain.Entities.Study>.Failure("study is null");
        return Result<Domain.Entities.Study>.Success(study);
    }
}