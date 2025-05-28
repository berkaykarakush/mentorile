using Mentorile.Services.Email.Domain.Entities;
using Mentorile.Services.Email.Domain.Exceptions;
using Mentorile.Services.Email.Domain.Interfaces;
using Mentorile.Services.Email.Infrastructure.Persistence;
using Mentorile.Shared.Common;
using Mentorile.Shared.Services.Abstracts;
using Microsoft.EntityFrameworkCore;

namespace Mentorile.Services.Email.Infrastructure.Repository;

public class EmailUserRepository : IEmailUserRepository
{
    private readonly AppDbContext _appDbContext;
    private readonly IExecutor _executor;
    public EmailUserRepository(AppDbContext appDbContext, IExecutor executor)
    {
        _appDbContext = appDbContext;
        _executor = executor;
    }

    public Task<Result<bool>> AddAsync(EmailUser user)
        => _executor.ExecuteAsync(async () =>
        {
            await _appDbContext.EmailUsers.AddAsync(user);
            var result = await _appDbContext.SaveChangesAsync() > 0;
            return result;
        });

    public Task<Result<EmailUser>> GetByIdAsync(string userId)
        => _executor.ExecuteAsync(async () =>
        {
            var user = await _appDbContext.EmailUsers.FirstOrDefaultAsync(u => u.UserId == userId);
            if (user == null) throw new UserNotFoundException();
            return user;
        });
}