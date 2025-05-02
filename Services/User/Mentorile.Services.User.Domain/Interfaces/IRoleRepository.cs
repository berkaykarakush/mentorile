using Mentorile.Services.User.Domain.Entities;
using Mentorile.Shared.Common;

namespace Mentorile.Services.User.Domain.Interfaces;
public interface IRoleRepository
{
    Task<Result<Role>> GetByIdAsync(Guid id);
    Task<Result<Role>> GetByNameAsync(string roleName);
    Task<Result<PagedResult<Role>>> GetAllAsync(PagingParams pagingParams);
    Task<Result<bool>> AddAsync(Role role);
    Task<Result<bool>> UpdateAsync(Role role);
    Task<Result<bool>> DeleteAsync(Role role);
}
// public interface IRoleService
// {
//     /// <summary>
//     /// Yeni bir rol oluşturur.
//     /// </summary>
//     Task<Result<bool>> CreateRoleAsync(string roleName);

//     /// <summary>
//     /// Belirli bir rol sistemde var mı kontrol eder.
//     /// </summary>
//     Task<Result<bool>> RoleExistsAsync(string roleName);

//     /// <summary>
//     /// Tüm rollerin listesini döner.
//     /// </summary>
//     Task<Result<IEnumerable<ApplicationRole>>> GetAllRolesAsync();

//     /// <summary>
//     /// Belirli bir kullanıcıya rol ataması yapar.
//     /// </summary>
//     Task<Result<bool>> AssignRoleToUserAsync(Guid userId, string roleName);

//     /// <summary>
//     /// Kullanıcının rollerini döner.
//     /// </summary>
//     Task<Result<IEnumerable<string>>> GetRolesOfUserAsync(Guid userId);

//     /// <summary>
//     /// Kullanıcının belirli bir role sahip olup olmadığını kontrol eder.
//     /// </summary>
//     Task<Result<bool>> UserIsInRoleAsync(Guid userId, string roleName);

//     /// <summary>
//     /// Kullanıcıdan bir rolü kaldırır.
//     /// </summary>
//     Task<Result<bool>> RemoveRoleFromUserAsync(Guid userId, string roleName);
// }