using DEP_Blazor_WASM.Entities.Models;
using DEP_Blazor_WASM.Services.Interfaces;
using System.Net.Http.Json;

namespace DEP_Blazor_WASM.Services
{
    public class CourseService : ICourseService
    {
        private readonly HttpClient _httpClient;

        public CourseService(IHttpClientFactory factory)
        {
            _httpClient = factory.CreateClient("ServerApi");
        }

        public async Task<bool> AddCourseAsync(Course course)
        {
            var response = await _httpClient.PostAsJsonAsync("Courses", course);

            if (response.IsSuccessStatusCode)
            {
                // Deserialize the response body to a bool
                return await response.Content.ReadFromJsonAsync<bool>();
            }

            return false; // Indicate failure if the request was not successful
        }

        public async Task<List<Course>> GetCoursesAsync()
        {
            var response = await _httpClient.GetAsync("Courses");

            var courses = new List<Course>();
            if (response.IsSuccessStatusCode)
            {
                courses = await response.Content.ReadFromJsonAsync<List<Course>>() ?? new List<Course>();
            }

            return courses;
        }

        public async Task<List<Course>> GetCoursesByModuleAsync(int id)
        {
            var response = await _httpClient.GetAsync($"Courses/module/{id}");

            var courses = new List<Course>();
            if (response.IsSuccessStatusCode)
            {
                courses = await response.Content.ReadFromJsonAsync<List<Course>>() ?? new List<Course>();
            }

            return courses;
        }

        public async Task<bool> UpdateCourseAsync(Course model)
        {
            var response = await _httpClient.PutAsJsonAsync("Courses", model);

            if (response.IsSuccessStatusCode)
            {
                // Deserialize the response body to a bool
                return await response.Content.ReadFromJsonAsync<bool>();
            }

            return false;
        }

        public async Task<bool> DeleteCourseAsync(int courseId)
        {
            var response = await _httpClient.DeleteAsync($"Courses/{courseId}");

            if (response.IsSuccessStatusCode)
            {
                // Deserialize the response body to a bool
                return await response.Content.ReadFromJsonAsync<bool>();
            }

            return false;
        }
    }
}
