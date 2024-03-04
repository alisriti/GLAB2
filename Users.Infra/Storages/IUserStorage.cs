using GLab.Domains.Models.Users;

namespace Users.Infra.Storages;

public interface IUserStorage
{
    Task<User?> SelectUserById(string userId);

    Task<User> SelectUserByUserName(string userName);

    Task<string> SelectUserPassword(string userId);

    Task<bool> InsertUser(User user);
}