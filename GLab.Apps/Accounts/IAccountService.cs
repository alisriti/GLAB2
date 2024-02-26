namespace GLab.Apps.Accounts;

public interface IAccountService
{
    Task<LoginStatus> CheckCredentials(string userName, string password);
}

public enum LoginStatus
{
    CanLogin = 1,
    UserBlocked = 0,
    UserNotExists = -1,
    WrongCredentials = -2
}