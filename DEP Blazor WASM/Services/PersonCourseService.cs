using DEP_Blazor_WASM.Entities.Models;
using DEP_Blazor_WASM.Services.Interfaces;
using System.Net.Http;
using System.Net.Http.Json;

namespace DEP_Blazor_WASM.Services
{
    public class PersonCourseService : IPersonCourseService
    {
        private readonly HttpClient _httpClient;

        public PersonCourseService(IHttpClientFactory factory)
        {
            _httpClient = factory.CreateClient("ServerApi");
        }

        public async Task<bool> AddPersonCourseAsync(PersonCourse personCourse)
        {
            var response = await _httpClient.PostAsJsonAsync("PersonCourses", personCourse);

            if (response.IsSuccessStatusCode)
            {
                // Deserialize the response body to a bool
                return await response.Content.ReadFromJsonAsync<bool>();
            }

            return false; // Indicate failure if the request was not successful
        }

        public async Task<bool> UpdatePersonCourseAsync(PersonCourse updatedPersonCourse)
        {
            try
            {
                // Make the PUT request to update the PersonCourse
                var response = await _httpClient.PutAsJsonAsync("PersonCourses", updatedPersonCourse);

                // Return true if the update was successful, otherwise false
                return response.IsSuccessStatusCode;
            }
            catch (Exception ex)
            {
                // Handle any errors (e.g., network issues)
                Console.WriteLine($"Error updating PersonCourse: {ex.Message}");
                return false;
            }
        }

        public async Task<bool> DeletePersonCourseAsync(int personId, int courseId)
        {
            try
            {
                // Make the DELETE request to delete the PersonCourse
                var response = await _httpClient.DeleteAsync($"PersonCourses/person/{personId}/course/{courseId}");

                // Return true if the delete was successful, otherwise false
                return response.IsSuccessStatusCode;
            }
            catch (Exception ex)
            {
                // Handle any errors (e.g., network issues)
                Console.WriteLine($"Error deleting PersonCourse: {ex.Message}");
                return false;
            }
        }

        public async Task<List<PersonCourse>> GetAllPersonCoursesForPersonAsync(int personId)
        {
            var response = await _httpClient.GetAsync($"PersonCourses/person/{personId}");

            var personCourses = new List<PersonCourse>();
            if (response.IsSuccessStatusCode)
            {
                personCourses = await response.Content.ReadFromJsonAsync<List<PersonCourse>>() ?? new List<PersonCourse>();
            }

            return personCourses;
        }
    }
}
