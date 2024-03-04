namespace GLab.Domains.Models.Users;

public class ApplicationRole
{
    public ApplicationRole()
    {
    }

    public ApplicationRole(int roleId, string roleName)
    {
        RoleId = roleId;
        RoleName = roleName;
    }

    public int RoleId { get; set; }
    public string RoleName { get; set; }
}