using GLab.Domains.Models.Users;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace GLAB2.Controllers
{
    [Route("identity")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        [HttpGet, Route("getdata")]
        public string GetData()
        {
            return
                "{\"UserId\":\"fdb96e0a-72a6-4a26-9fe2-2c08117153e7\",\"UserName\":\"Ali Sriti\",\"Email\":\"alisriti@gmail.com\",\"Roles\":[{\"RoleId\":1,\"RoleName\":\"mon role\"}]}";
        }

        [HttpPost, Route("dologin")]
        public async Task DoLogin(LoginClaims claims)
        {
            IEnumerable<Claim>? identityClaims = getClaims(claims);

            ClaimsIdentity identity = new(identityClaims, "auth");
            ClaimsPrincipal principal = new(identity);

            await HttpContext.SignInAsync(principal, new AuthenticationProperties()
            { IsPersistent = true });
        }

        private IEnumerable<Claim>? getClaims(LoginClaims claims)
        {
            List<Claim> claimsIdentity = new List<Claim>();
            claimsIdentity.Add(new Claim(ClaimTypes.Email, claims.UserName));
            claimsIdentity.Add(new Claim(ClaimTypes.Name, claims.UserName));
            claimsIdentity.Add(new Claim("UserId", claims.UserId));

            foreach (ApplicationRole role in claims.Roles)
            {
                claimsIdentity.Add(new Claim(ClaimTypes.Role, role.RoleName));
            }
            return claimsIdentity;
        }
    }
}