using Mentorile.Services.User.Domain.Entities;
using Mentorile.Services.User.Domain.Exceptions;
using Mentorile.Services.User.Domain.Interfaces;
using Mentorile.Services.User.Infrastructure.Persistence;
using Mentorile.Shared.Common;
using Mentorile.Shared.Services.Abstracts;
using Microsoft.EntityFrameworkCore;

namespace Mentorile.Services.User.Infrastructure.Repositories;
public class RoleRepository : IRoleRepository
{
    private readonly AppDbContext _appDbContext;
    private readonly IExecutor _executor;

    public RoleRepository(AppDbContext appDbContext, IExecutor executor)
    {
        _appDbContext = appDbContext;
        _executor = executor;
    }

    public Task<Result<bool>> AddAsync(Role role)
    => _executor.ExecuteAsync(async () =>{
        await _appDbContext.Roles.AddAsync(role);
        var result = await _appDbContext.SaveChangesAsync() > 0;
        return result;
    });

    public async Task<Result<bool>> DeleteAsync(Role role)
    => await _executor.ExecuteAsync(async () =>{
        _appDbContext.Roles.Remove(role);
        var result = await _appDbContext.SaveChangesAsync() > 0;
        return result;
    });

    public async Task<Result<PagedResult<Role>>> GetAllAsync(PagingParams pagingParams)
    => await _executor.ExecuteAsync(async () => {
        var query = _appDbContext.Roles;
        var totalCount = await query.CountAsync();
        var roles = await query
            .Skip((pagingParams.PageNumber - 1) * pagingParams.PageSize)
            .Take(pagingParams.PageSize)
            .ToListAsync();
        var pagedRoles = PagedResult<Role>.Create(roles, totalCount, pagingParams);
        return pagedRoles;
    });

    public async Task<Result<Role>> GetByIdAsync(Guid id)
    => await _executor.ExecuteAsync(async () => {
        var role = await _appDbContext.Roles.FindAsync(id);
        if(role == null) throw new RoleNotFoundException();
        return role;
    });
    public async Task<Result<Role>> GetByNameAsync(string roleName)
    => await _executor.ExecuteAsync(async () => {
        var role = await _appDbContext.Roles.FirstOrDefaultAsync(r => r.Name == roleName);
        if(role == null) throw new RoleNotFoundException();
        return role;
    });

    public async Task<Result<bool>> UpdateAsync(Role role)
    => await _executor.ExecuteAsync(async () => {
        _appDbContext.Roles.Update(role);
        var result = await _appDbContext.SaveChangesAsync() > 0;
        return result;
    });
}