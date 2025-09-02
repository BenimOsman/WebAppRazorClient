using System.Net.Http;                                                                                  // Import the HttpClient class for making HTTP requests
using System.Net.Http.Json;                                                                             // Import the extension methods to handle JSON content in HttpClient
using System.Threading.Tasks;                                                                           // Import Task and async functionality for asynchronous operations

namespace WebAppRazorSandwitchClient
{
    public class AuthService                                                                            
    {
        private readonly HttpClient _httpClient;                                                        // Private field to store the injected HttpClient instance

        public AuthService(HttpClient httpClient)                                                       // Dependency Injection and initializes the _httpClient field
        {
            _httpClient = httpClient;                                                                   // Assign the injected HttpClient to the private field
        }

        // For Registering a new user 
        public async Task<bool> RegisterAsync(RegisterViewModel model)
        {
            var response = await _httpClient.PostAsJsonAsync("https://localhost:7281/register", model); // Sends a POST request to the Register endpoint
            return response.IsSuccessStatusCode;                                                        // It returns a bool indicating the success or failure
        }

        // For logging in an existing user
        public async Task<bool> LoginAsync(LoginViewModel model)
        {
            var response = await _httpClient.PostAsJsonAsync("https://localhost:7281/login", model);    // Sends a POST request to the Login endpoint 
            return response.IsSuccessStatusCode;                                                        // It returns a bool indicating the success or failure
        }
    }
}



// AuthService class that is responsible for handling authentication-related API calls.
// Specifically, it has methods for registering a new user and logging in an existing user by making HTTP requests to the backend API.