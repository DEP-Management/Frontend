using DEP_Blazor_WASM.Entities.Models;
using DEP_Blazor_WASM.Entities.RequestModels;
using DEP_Blazor_WASM.Entities.ViewModels;
using DEP_Blazor_WASM.Services.Interfaces;
using System.Net.Http.Json;

namespace DEP_Blazor_WASM.Services
{
    public class ModuleService : IModuleService
    {
        private readonly HttpClient _httpClient;

        public ModuleService(IHttpClientFactory factory)
        {
            _httpClient = factory.CreateClient("ServerApi");
        }

        public async Task<List<Module>> GetModulesAsync()
        {
            var response = await _httpClient.GetAsync("Module");

            var modules = new List<Module>();
            if (response.IsSuccessStatusCode)
            {
                modules = await response.Content.ReadFromJsonAsync<List<Module>>() ?? new List<Module>();
            }

            return modules;
        }

        public async Task<List<ModuleWithCourseViewModel>> GetModulesWithCoursesAsync()
        {
            var response = await _httpClient.GetAsync("Module/moduleswithcourse");

            var modules = new List<ModuleWithCourseViewModel>();
            if (response.IsSuccessStatusCode)
            {
                modules = await response.Content.ReadFromJsonAsync<List<ModuleWithCourseViewModel>>() ?? new List<ModuleWithCourseViewModel>();
            }

            return modules;
        }

        public async Task<bool> AddModuleAsync(Module module)
        {
            var response = await _httpClient.PostAsJsonAsync("Module", module);

            if (response.IsSuccessStatusCode)
            {
                // Deserialize the response body to a bool
                return await response.Content.ReadFromJsonAsync<bool>();
            }

            return false; // Indicate failure if the request was not successful
        }

        public async Task<bool> UpdateModuleAsync(Module model)
        {
            var response = await _httpClient.PutAsJsonAsync("Module", model);

            if (response.IsSuccessStatusCode)
            {
                // Deserialize the response body to a bool
                return await response.Content.ReadFromJsonAsync<bool>();
            }

            return false;
        }

        public async Task<bool> DeleteModuleAsync(int moduleId)
        {
            var response = await _httpClient.DeleteAsync($"Module/{moduleId}");

            if (response.IsSuccessStatusCode)
            {
                // Deserialize the response body to a bool
                return await response.Content.ReadFromJsonAsync<bool>();
            }

            return false;
        }
    }
}
