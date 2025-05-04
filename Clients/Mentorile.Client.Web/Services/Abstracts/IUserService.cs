using Mentorile.Client.Web.ViewModels.Users;
using Mentorile.Shared.Common;

namespace Mentorile.Client.Web.Services.Abstracts;
public interface IUserService
{
    Task<UserViewModel> GetUser(string userId);
    Task<List<UserViewModel>> GetAllUsers();
}