using GLab.Apps.Accounts;
using GLab.Apps.Users;
using GLab.Domains.Models.Users;

namespace GLab.Impl.Services.Accounts
{
    public class AccountService : IAccountService
    {
        private readonly IUserService userService;

        public AccountService(IUserService userService)
        {
            this.userService = userService;
        }

        public async Task<LoginStatus> CheckCredentials(string userName, string password)
        {
            User user = await userService.GetUserByUserName(userName);

            if (user == null || user.State == UserState.Deleted)
                return LoginStatus.UserNotExists;

            if (user.State == UserState.Bloqued)
                return LoginStatus.UserBlocked;

            bool isPasswordCorrect = await userService.ValidatePassword(user.UserId, password);

            if (isPasswordCorrect)
                return LoginStatus.CanLogin;
            else return LoginStatus.WrongCredentials;
        }

        public async Task<LoginClaims?> GetUserClaims(string userId)
        {
            User? user = await userService.GetUserById(userId);

            if (user is null)
                return null;

            List<ApplicationRole> roles = await userService.GetRoles();

            ApplicationRole teamMember = roles.First(r => r.RoleId == 3);

            LoginClaims claims = new LoginClaims()
            {
                UserId = user.UserId,
                UserName = user.UserName,
                Email = user.UserName,
            };
            claims.Roles.Add(teamMember);

            return claims;
        }
    }
}