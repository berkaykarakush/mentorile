using Mentorile.Client.Web.ViewModels.Users;

namespace Mentorile.Client.Web.Services.Abstracts;
public interface IUserService
{
    Task<UserViewModel> GetUser();
}