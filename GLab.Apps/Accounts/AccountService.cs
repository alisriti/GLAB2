using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GLab.Apps.Users;
using GLab.Domains.Models.Users;

namespace GLab.Apps.Accounts
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
    }
}