using DEP_Blazor_WASM.Entities.Models;
using DEP_Blazor_WASM.Entities.RequestModels;
using DEP_Blazor_WASM.Services.Interfaces;
using System.Net.Http.Json;

namespace DEP_Blazor_WASM.Services
{
    public class DepartmentService : IDepartmentService
    {
        private readonly HttpClient _httpClient;

        public DepartmentService(IHttpClientFactory factory)
        {
            _httpClient = factory.CreateClient("ServerApi");
        }

        public async Task<bool> AddDepartmentAsync(Department department)
        {
            var response = await _httpClient.PostAsJsonAsync("Departments", department);

            if (response.IsSuccessStatusCode)
            {
                // Deserialize the response body to a bool
                return await response.Content.ReadFromJsonAsync<bool>();
            }

            return false; // Indicate failure if the request was not successful
        }

        public async Task<List<Department>> GetDepartmentsAsync()
        {
            try
            {
                var departments = await _httpClient.GetFromJsonAsync<List<Department>>("departments");

                return departments ?? new List<Department>();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Exception from GetDepartmentsAsync: {ex.Message}");
            }

            // Return an empty list in case no data is found and the response is null
            return new List<Department>();
        }

        public async Task<bool> UpdateDepartmentAsync(Department model)
        {
            var response = await _httpClient.PutAsJsonAsync("departments", model);

            if (response.IsSuccessStatusCode)
            {
                // Deserialize the response body to a bool
                return await response.Content.ReadFromJsonAsync<bool>();
            }

            return false;
        }

        public async Task<bool> DeleteDepartmentAsync(int departmentId)
        {
            var response = await _httpClient.DeleteAsync($"departments/{departmentId}");

            if (response.IsSuccessStatusCode)
            {
                // Deserialize the response body to a bool
                return await response.Content.ReadFromJsonAsync<bool>();
            }

            return false;
        }

    }
}
