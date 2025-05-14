using DEP_Blazor_WASM.Entities.Models;
using DEP_Blazor_WASM.Entities.ViewModels;
using DEP_Blazor_WASM.Services.Interfaces;
using System.Net.Http.Json;

namespace DEP_Blazor_WASM.Services
{
    public class StatisticsService : IStatisticsService
    {
        private readonly HttpClient _httpClient;

        public StatisticsService(IHttpClientFactory factory)
        {
            _httpClient = factory.CreateClient("ServerApi");
        }

        public async Task<List<PersonPerDepartmentViewModel>> GetPersonsPerDepartmentAsync()
        {
            var response = await _httpClient.GetAsync("Statistics/personsperdepartment");

            var viewModels = new List<PersonPerDepartmentViewModel>();
            if (response.IsSuccessStatusCode)
            {
                viewModels = await response.Content.ReadFromJsonAsync<List<PersonPerDepartmentViewModel>>() ?? new List<PersonPerDepartmentViewModel>();
            }

            return viewModels;
        }

        public async Task<List<PersonPerDepartmentViewModel>> GetPersonsPerDepartmentByModuleIdAsync(int moduleId)
        {
            var response = await _httpClient.GetAsync($"Statistics/personsperdepartment/module/{moduleId}");

            var viewModels = new List<PersonPerDepartmentViewModel>();
            if (response.IsSuccessStatusCode)
            {
                viewModels = await response.Content.ReadFromJsonAsync<List<PersonPerDepartmentViewModel>>() ?? new List<PersonPerDepartmentViewModel>();
            }

            return viewModels;
        }

        public async Task<List<PersonPerLocationViewModel>> GetPersonsPerLocationAsync()
        {
            var response = await _httpClient.GetAsync("Statistics/personsperlocation");

            var viewModels = new List<PersonPerLocationViewModel>();
            if (response.IsSuccessStatusCode)
            {
                viewModels = await response.Content.ReadFromJsonAsync<List<PersonPerLocationViewModel>>() ?? new List<PersonPerLocationViewModel>();
            }

            return viewModels;
        }

        public async Task<List<CourseStatusCountViewModel>> GetPersonsPerCourseStatusByModuleIdAsync(int moduleId)
        {
            var response = await _httpClient.GetAsync($"Statistics/coursestatuscount/module/{moduleId}");

            var viewModels = new List<CourseStatusCountViewModel>();
            if (response.IsSuccessStatusCode)
            {
                viewModels = await response.Content.ReadFromJsonAsync<List<CourseStatusCountViewModel>>() ?? new List<CourseStatusCountViewModel>();
            }

            return viewModels;
        }
    }
}