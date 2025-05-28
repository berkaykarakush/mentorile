using Mentorile.Services.Email.Domain.Entities;
using Mentorile.Shared.Common;

namespace Mentorile.Services.Email.Domain.Interfaces;
public interface IEmailUserRepository
{
    Task<Result<bool>> AddAsync(EmailUser user);
    Task<Result<EmailUser>> GetByIdAsync(string userId);
}