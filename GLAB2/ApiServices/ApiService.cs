using GLab.Domains.Models.Users;
using System.Text;
using System.Text.Json;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Mvc;

namespace GLAB2.ApiServices
{
    public class ApiService
    {
        private readonly HttpClient client;

        public ApiService(NavigationManager navigationManager)
        {
            client = new HttpClient();
            client.BaseAddress = new Uri(navigationManager?.BaseUri);
        }

        public async ValueTask<ActionResult> SignIn(LoginClaims claims)
        {
            var content = new StringContent(JsonSerializer.Serialize(claims), Encoding.UTF8, "application/json");

            var response = await client.PostAsync("identity/dologin", content);

            return response.IsSuccessStatusCode ? new OkResult() : new BadRequestResult();
        }
    }
}