using GLab.Apps.Users;
using GLab.Domains.Models.Users;
using Users.Infra.Storages;

namespace GLab.Impl.Services.Users;

public class UserService : IUserService
{
    private readonly IUserStorage userStorage;
    private PasswordHasher passwordHasher;

    public UserService(IUserStorage userStorage)
    {
        this.userStorage = userStorage;
        passwordHasher = new PasswordHasher();
    }

    public async Task<User> GetUserById(string userId)
    {
        return await userStorage.SelectUserById(userId);
    }

    public async Task<User> GetUserByUserName(string userName)
    {
        return await userStorage.SelectUserByUserName(userName);
    }

    public async Task<bool> ValidatePassword(string userId, string password)
    {
        string hashedPassword = await userStorage.SelectUserPassword(userId);
        return passwordHasher.VerifyHashedPassword(hashedPassword, password);
    }
}