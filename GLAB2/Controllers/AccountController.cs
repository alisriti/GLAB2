using GLab.Domains.Models.Users;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace GLAB2.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        [HttpPost]
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