using Dima.Core.BaseResponses;
using Dima.Core.Handlers;
using Dima.Core.Requests.Account;
using System.Net.Http.Json;
using System.Text;

namespace Dima.Web.Handlers
{
    public class AccountHandler(IHttpClientFactory httpClientFactory) : IAccountHandler
    {
        private readonly HttpClient _httpClient = httpClientFactory.CreateClient(Configuration.HttpClientName);

        public async Task<BaseResponse<string>> LoginAsync(LoginRequest request)
        {
            var result = await _httpClient.PostAsJsonAsync("v1/identity/login?useCookies=true", request);

            return result.IsSuccessStatusCode
                ? new BaseResponse<string>("Login realizado com sucesso!", 200, "Login realizado com sucesso!")
                : new BaseResponse<string>(null, 400, "Não foi possivel realizar o login");
        }

        public async Task<BaseResponse<string>> RegisterAsync(RegisterRequest request)
        {
            var result = await _httpClient.PostAsJsonAsync("v1/identity/register", request);
            return result.IsSuccessStatusCode
                ? new BaseResponse<string>("Cadastro realizado com sucesso!", 201, "Cadastro realizado com sucesso!")
                : new BaseResponse<string>(null, 400, "Não foi possível realizar o seu cadastro");
        }

        public async Task LogoutAsync()
        {
            var emptyContent = new StringContent("{}", Encoding.UTF8, "application/json");
            await _httpClient.PostAsJsonAsync("v1/identity/logout", emptyContent);
        }
    }
}
