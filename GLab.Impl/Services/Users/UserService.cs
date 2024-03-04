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

    public async Task<User?> GetUserById(string userId)
    {
        return await userStorage.SelectUserById(userId);
    }

    public async Task<User?> GetUserByUserName(string userName)
    {
        return await userStorage.SelectUserByUserName(userName);
    }

    public async Task<bool> ValidatePassword(string userId, string password)
    {
        string hashedPassword = await userStorage.SelectUserPassword(userId);
        return passwordHasher.VerifyHashedPassword(hashedPassword, password);
    }

    public async Task<List<ApplicationRole>> GetRoles()
    {
        return new List<ApplicationRole>()
        {
            new ApplicationRole()
            {
                RoleId = 3,
                RoleName = "TeamMember"
            }
        };
    }

    public async Task CreateUser(User user)
    {
        validateUserForCreation(user);
        validateUserNameDoesNotExists(user.UserName);
        await userStorage.InsertUser(user);
    }

    private void validateUserNameDoesNotExists(string userName)
    {
        if (userStorage.SelectUserByUserName(userName).GetAwaiter().GetResult() is not null)
            throw new Exception($"Username {userName} existe déja");
    }

    private void validateUserForCreation(User user)
    {
        if (user is null)
            throw new Exception("Aucun utilisateur n'est fourni");

        if (string.IsNullOrWhiteSpace(user.UserId) ||
            string.IsNullOrWhiteSpace(user.UserName) ||
            string.IsNullOrWhiteSpace(user.Password))
            throw new Exception("Erreur de validation de l'utilisateur");
    }
}