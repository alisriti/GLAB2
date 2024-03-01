namespace GLab.Domains.Models.Users
{
    public class LoginClaims
    {
        public string UserId { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; }
        public List<ApplicationRole> Roles { get; set; }
    }
}