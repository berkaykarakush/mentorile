using Mentorile.Services.Email.Domain.Entities;
using Mentorile.Services.Email.Domain.Interfaces;
using Mentorile.Services.Email.Infrastructure.Persistence;
using Mentorile.Shared.Common;
using Mentorile.Shared.Services.Abstracts;

namespace Mentorile.Services.Email.Infrastructure.Repository;

public class EmailLogRepository : IEmailLogRepository
{
    private readonly AppDbContext _appDbContext;
    private readonly IExecutor _executor;
    public EmailLogRepository(AppDbContext appDbContext, IExecutor executor)
    {
        _appDbContext = appDbContext;
        _executor = executor;
    }

    public async Task<Result<bool>> AddAsync(EmailLog log)
        => await _executor.ExecuteAsync(async () =>
        {
            await _appDbContext.EmailLogs.AddAsync(log);
            var result = await _appDbContext.SaveChangesAsync() > 0;
            return result;
        });
}