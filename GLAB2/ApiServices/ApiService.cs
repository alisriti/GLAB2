using GLab.Domains.Models.Users;
using System.Text;
using System.Text.Json;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

namespace GLAB2.ApiServices
{
    public class ApiService
    {
        private readonly IHttpClientFactory _factory;

        public ApiService(IHttpClientFactory factory)
        {
            _factory = factory;
        }

        [HttpPost]
        public async ValueTask<IActionResult> SignIn(LoginClaims claims)
        {
            var client = _factory.CreateClient("Account");

            var content = new StringContent(JsonSerializer.Serialize(claims), Encoding.UTF8, "application/json");

            var response = await client.PostAsync(client.BaseAddress, content);

            return response.IsSuccessStatusCode ? new OkResult() : new BadRequestResult();
        }
    }
}