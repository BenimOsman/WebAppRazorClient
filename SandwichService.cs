using System.Text.Json;

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

        public async Task<SandwichModel> AddSandwich(SandwichModel sandwich)
        {
            try
            {
                using HttpClient client = new HttpClient();
                var response = await client.PostAsJsonAsync("https://localhost:7281/api/Sandwich", sandwich);

                response.EnsureSuccessStatusCode();
                var createdSandwich = await response.Content.ReadFromJsonAsync<SandwichModel>();
                return createdSandwich;
            }
            catch (Exception ex)
            {
                throw;
            }
        }


    }
}