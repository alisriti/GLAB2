using GLab.Domains.Models.Users;

namespace GLab.Apps.Users;

public interface IUserService
{
    Task<User?> GetUserById(string userId);

    Task<User?> GetUserByUserName(string userName);

    Task<bool> ValidatePassword(string userId, string password);

    Task<List<ApplicationRole>> GetRoles();

    Task CreateUser(User user);
}