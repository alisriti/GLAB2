namespace GLab.Impl.Services.Users;

public class PasswordHasher
{
    private static readonly string pass = "her3y4g0Kr1s";

    public string HashPassword(string password)
    {
        return BCrypt.HashPassword(password + pass, BCrypt.GenerateSalt());
    }

    public bool VerifyHashedPassword(string hashedPassword, string providedPassword)
    {
        return !string.IsNullOrEmpty(providedPassword) && !string.IsNullOrEmpty(hashedPassword) &&
               BCrypt.CheckPassword(providedPassword + pass, hashedPassword);
    }
}