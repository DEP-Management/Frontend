using DEP_Blazor_WASM.Entities.Enums;
using DEP_Blazor_WASM.Entities.Models;
using DEP_Blazor_WASM.Services.Interfaces;
using System.Net.Http.Json;

namespace DEP_Blazor_WASM.Services
{
    public class LocationService : ILocationService
    {
        private readonly HttpClient _httpClient;

        public LocationService(IHttpClientFactory factory)
        {
            _httpClient = factory.CreateClient("ServerApi");
        }

        public async Task<bool> AddLocationAsync(Location location)
        {
            var response = await _httpClient.PostAsJsonAsync("location", location);

            if (response.IsSuccessStatusCode)
            {
                // Deserialize the response body to a bool
                return await response.Content.ReadFromJsonAsync<bool>();
            }

            return false; // Indicate failure if the request was not successful
        }

        public async Task<List<Location>> GetLocationsAsync()
        {
            var response = await _httpClient.GetAsync("location");

            var locations = new List<Location>();
            if (response.IsSuccessStatusCode)
            {
                locations = await response.Content.ReadFromJsonAsync<List<Location>>() ?? new List<Location>();
            }

            return locations;
        }

        public async Task<bool> UpdateLocationAsync(Location model)
        {
            var response = await _httpClient.PutAsJsonAsync("location", model);

            if (response.IsSuccessStatusCode)
            {
                // Deserialize the response body to a bool
                return await response.Content.ReadFromJsonAsync<bool>();
            }

            return false;
        }

        public async Task<bool> DeleteLocationAsync(int locationId)
        {
            var response = await _httpClient.DeleteAsync($"location/{locationId}");

            if (response.IsSuccessStatusCode)
            {
                // Deserialize the response body to a bool
                return await response.Content.ReadFromJsonAsync<bool>();
            }

            return false;
        }
    }
}
