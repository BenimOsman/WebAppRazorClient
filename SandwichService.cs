// Communication bridge between the razor pages and the Web API

using System.Text;                                                              // For Encoding used in JSON content if needed
using System.Text.Json;                                                         // For JSON serialization/deserialization
using WebAppRazorClient;                                                        // Access SandwichModel

namespace WebAppRazorClient
{
    public class SandwichService
    {
        public SandwichService() { }                                            // Empty constructor, service can be injected if needed

        // Get all sandwiches from API
        public async Task<List<SandwichModel>> GetSandwiches()
        {
            try
            {
                // Handler to bypass SSL certificate validation (for localhost/self-signed)
                HttpClientHandler handler = new HttpClientHandler
                {
                    ServerCertificateCustomValidationCallback = (message, cert, chain, errors) => true
                };

                using HttpClient client = new HttpClient(handler);      // HTTP client instance
                await using Stream stream = await client.GetStreamAsync("https://localhost:7281/api/Sandwich");  // Fetch stream from API

                var sandwiches = await JsonSerializer.DeserializeAsync<List<SandwichModel>>(stream);  // Deserialize JSON into List<SandwichModel>
                return sandwiches ?? new List<SandwichModel>();       // Return empty list if null
            }
            catch (Exception ex)
            {
                // Log or handle the exception if needed
                throw;
            }
        }

        // Add a new sandwich
        public async Task<SandwichModel?> AddSandwitch(SandwichModel sandwitch)
        {
            try
            {
                HttpClientHandler handler = new HttpClientHandler
                {
                    ServerCertificateCustomValidationCallback = (message, cert, chain, errors) => true
                };

                using HttpClient client = new HttpClient(handler);                        // Create HTTP client
                var response = await client.PostAsJsonAsync("https://localhost:7281/api/Sandwich", sandwitch);  // POST sandwich data

                if (!response.IsSuccessStatusCode)                                        // Check if request failed
                {
                    var errorContent = await response.Content.ReadAsStringAsync();          // Optional: read error message
                    Console.WriteLine($"Error: {response.StatusCode}, Content: {errorContent}");
                    return null;                                                         // Return null if failed
                }

                var createsandwitch = await response.Content.ReadFromJsonAsync<SandwichModel>();  // Deserialize created sandwich
                return createsandwitch;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Exception: {ex.Message}");  // Log exception message
                throw;
            }
        }

        // Get sandwich by ID
        public async Task<SandwichModel?> GetSandwitchByIdAsync(int id)
        {
            try
            {
                HttpClientHandler handler = new HttpClientHandler
                {
                    ServerCertificateCustomValidationCallback = (message, cert, chain, errors) => true
                };

                using HttpClient client = new HttpClient(handler);                    // HTTP client instance
                var response = await client.GetAsync($"https://localhost:7281/api/Sandwich/{id}"); // GET sandwich by id

                if (!response.IsSuccessStatusCode) return null;                      // Return null if not found

                var sandwich = await response.Content.ReadFromJsonAsync<SandwichModel>(); // Deserialize sandwich
                return sandwich;
            }
            catch (Exception)
            {
                throw;  // Re-throw exception for higher-level handling
            }
        }

        // Update an existing sandwich
        public async Task<bool> UpdateSandwitchAsync(SandwichModel sandwich)
        {
            using HttpClient client = new HttpClient();                             // HTTP client instance
            var response = await client.PutAsJsonAsync($"https://localhost:7281/api/Sandwich/{sandwich.Id}", sandwich); // PUT updated data

            return response.IsSuccessStatusCode;                                    // Return true if update succeeded
        }

        // Delete a sandwich by ID
        public async Task<bool> DeleteSandwitchAsync(int id)
        {
            try
            {
                HttpClientHandler handler = new HttpClientHandler
                {
                    ServerCertificateCustomValidationCallback = (message, cert, chain, errors) => true
                };

                using HttpClient client = new HttpClient(handler);                 // HTTP client instance
                var response = await client.DeleteAsync($"https://localhost:7281/api/Sandwich/{id}");  // DELETE request

                return response.IsSuccessStatusCode;                                // Return true if deletion succeeded
            }
            catch (Exception)
            {
                throw;  // Re-throw exception for higher-level handling
            }
        }
    }
}