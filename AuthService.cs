using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;

namespace WebAppRazorSandwitchClient
{

    public class AuthService
    {
        private readonly HttpClient _httpClient;

        public AuthService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<bool> RegisterAsync(RegisterViewModel model)
        {
            var response = await _httpClient.PostAsJsonAsync("https://localhost:7281/register", model);
            return response.IsSuccessStatusCode;
        }

        public async Task<bool> LoginAsync(LoginViewModel model)
        {
            var response = await _httpClient.PostAsJsonAsync("https://localhost:7281/login", model);
            return response.IsSuccessStatusCode;
        }


    }

}