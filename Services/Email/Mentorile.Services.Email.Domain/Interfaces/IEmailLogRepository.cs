using Mentorile.Services.Email.Domain.Entities;
using Mentorile.Shared.Common;

namespace Mentorile.Services.Email.Domain.Interfaces;
public interface IEmailLogRepository
{
    Task<Result<bool>> AddAsync(EmailLog log);    
}