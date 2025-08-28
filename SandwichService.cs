using System.Text;
using System.Text.Json;
using WebAppRazorClient;

namespace WebAppRazorClient
{
    public class SandwichService
    {
        public SandwichService() { }

        public async Task<List<SandwichModel>> GetSandwiches()
        {
            try
            {
                HttpClientHandler handler = new HttpClientHandler
                {
                    ServerCertificateCustomValidationCallback = (message, cert, chain, errors) => true
                };

                HttpClient client = new HttpClient(handler);
                await using Stream stream = await client.GetStreamAsync("https://localhost:7281/api/Sandwich");

                var sandwiches = await JsonSerializer.DeserializeAsync<List<SandwichModel>>(stream);
                return sandwiches ?? new List<SandwichModel>();
            }
            catch (Exception ex)
            {
                // Consider logging the exception here
                throw;
            }
        }
        //public async Task<SandwitchModel> GetSandwichByIdAsync(int id)
        //{
        //    HttpClient client = new HttpClient();
        //    var response = await client.GetAsync("https://localhost:7281/api/Sandwich");
        //    response.EnsureSuccessStatusCode();
        //    var stream = await response.Content.ReadAsStreamAsync();
        //    return await JsonSerializer.DeserializeAsync<SandwitchModel>(stream);
        //}

        //add sandwitch function    
        //public async Task<SandwitchModel> AddSandwitch(SandwitchModel sandwitch)
        //{
        //    try
        //    {
        //        HttpClientHandler handler = new HttpClientHandler
        //        {
        //            ServerCertificateCustomValidationCallback = (message, cert, chain, errors) => true
        //        };
        //        using HttpClient client = new HttpClient(handler);
        //        var response = await client.PostAsJsonAsync("https://localhost:7281/api/Sandwich", sandwitch);
        //        //response.EnsureSuccessStatusCode();
        //        var createsandwitch = await response.Content.ReadFromJsonAsync<SandwitchModel>();
        //        return createsandwitch;
        //    }
        //    catch (Exception ex)
        //    {
        //        throw;
        //    }
        //}
        public async Task<SandwichModel?> AddSandwitch(SandwichModel sandwitch)
        {
            try
            {
                HttpClientHandler handler = new HttpClientHandler
                {
                    ServerCertificateCustomValidationCallback = (message, cert, chain, errors) => true
                };

                using HttpClient client = new HttpClient(handler);
                var response = await client.PostAsJsonAsync("https://localhost:7281/api/Sandwich", sandwitch);

                if (!response.IsSuccessStatusCode)
                {
                    // Optionally log or inspect the error response
                    var errorContent = await response.Content.ReadAsStringAsync();
                    Console.WriteLine($"Error: {response.StatusCode}, Content: {errorContent}");
                    return null;
                }

                var createsandwitch = await response.Content.ReadFromJsonAsync<SandwichModel>();
                return createsandwitch;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Exception: {ex.Message}");
                throw;
            }
        }
        //public async Task UpdateSandwichAsync(SandwitchModel sandwich)
        //{
        //    HttpClient client = new HttpClient();
        //    var content = new StringContent(JsonSerializer.Serialize(sandwich), Encoding.UTF8, "application/json");
        //    var response = await client.PutAsync("https://localhost:7281/api/Sandwich", content);
        //    response.EnsureSuccessStatusCode();
        //}
        public async Task<SandwichModel?> GetSandwitchByIdAsync(int id)
        {
            try
            {
                HttpClientHandler handler = new HttpClientHandler
                {
                    ServerCertificateCustomValidationCallback = (message, cert, chain, errors) => true
                };

                using HttpClient client = new HttpClient(handler);
                var response = await client.GetAsync($"https://localhost:7281/api/Sandwich/{id}");

                if (!response.IsSuccessStatusCode)
                    return null;

                var sandwich = await response.Content.ReadFromJsonAsync<SandwichModel>();
                return sandwich;
            }
            catch (Exception)
            {
                throw;
            }
        }
        public async Task<bool> UpdateSandwitchAsync(SandwichModel sandwich)
        {
            using HttpClient client = new HttpClient();
            var response = await client.PutAsJsonAsync($"https://localhost:7281/api/Sandwich/{sandwich.Id}", sandwich);

            // Just check if request succeeded
            return response.IsSuccessStatusCode;
        }

        public async Task<bool> DeleteSandwitchAsync(int id)
        {
            try
            {
                HttpClientHandler handler = new HttpClientHandler
                {
                    ServerCertificateCustomValidationCallback = (message, cert, chain, errors) => true
                };

                using HttpClient client = new HttpClient(handler);
                var response = await client.DeleteAsync($"https://localhost:7281/api/Sandwich/{id}");

                return response.IsSuccessStatusCode;
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}