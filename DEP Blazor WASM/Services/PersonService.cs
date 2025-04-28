using DEP_Blazor_WASM.Entities.Enums;
using DEP_Blazor_WASM.Entities.Models;
using DEP_Blazor_WASM.Entities.RequestModels;
using DEP_Blazor_WASM.Entities.ViewModels;
using DEP_Blazor_WASM.Services.Interfaces;
using System.Globalization;
using System.Net.Http.Json;

namespace DEP_Blazor_WASM.Services
{
    public class PersonService : IPersonService
    {
        private readonly HttpClient _httpClient;

        public PersonService(IHttpClientFactory factory)
        {
            _httpClient = factory.CreateClient("ServerApi");
        }

        public async Task<ApiResponse<T>> AddPersonAsync<T>(Person person)
        {
            try
            {
                var response = await _httpClient.PostAsJsonAsync("person", person);

                var apiResponse = await response.Content.ReadFromJsonAsync<ApiResponse<T>>();

                if (apiResponse is null)
                {
                    throw new Exception("Invalid response format.");
                }

                return apiResponse;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Exception in AddUserAsync: {ex.Message}");
                return new ApiResponse<T>
                {
                    Success = false,
                    Message = "An unexpected error occurred."
                };
            }
        }

        public async Task<List<Person>> GetPersonsAsync()
        {
            var response = await _httpClient.GetAsync("person");

            var persons = new List<Person>();
            if (response.IsSuccessStatusCode)
            {
                persons = await response.Content.ReadFromJsonAsync<List<Person>>() ?? new List<Person>();
            }

            foreach (var person in persons)
            {
                person.CompletedModules = person.PersonCourses.Count(pc => pc.Status == Status.Bestået);
            }

            return persons;
        }

        public async Task<List<Person>> GetPersonsByCourseIdAsync(int courseId)
        {
            var response = await _httpClient.GetAsync($"person/courseId/{courseId}");

            var persons = new List<Person>();
            if (response.IsSuccessStatusCode)
            {
                persons = await response.Content.ReadFromJsonAsync<List<Person>>() ?? new List<Person>();
            }

            return persons;
        }


        public async Task<Person?> GetPersonByIdAsync(int personId)
        {
            var response = await _httpClient.GetAsync($"person/{personId}");

            if (response.IsSuccessStatusCode)
            {
                return await response.Content.ReadFromJsonAsync<Person>();
            }

            return null;
        }

        public async Task<bool> UpdatePersonAsync(Person model)
        {
            var response = await _httpClient.PutAsJsonAsync("person", model);

            if (response.IsSuccessStatusCode)
            {
                // Deserialize the response body to a bool
                return await response.Content.ReadFromJsonAsync<bool>();
            }

            return false;
        }

        public async Task<bool> DeletePersonAsync(int personId)
        {
            var response = await _httpClient.DeleteAsync($"person/{personId}");

            if (response.IsSuccessStatusCode)
            {
                // Deserialize the response body to a bool
                return await response.Content.ReadFromJsonAsync<bool>();
            }

            return false;
        }

        public void ComputeEndDateStyles<T>(List<T> persons) where T : IHasEndDate
        {
            if (persons == null || persons.Count == 0)
            {
                return; // Exit if there are no persons to process
            }

            var today = DateTime.Now;

            foreach (var person in persons)
            {
                var hireDate = person.HiringDate;
                var endDate = person.EndDate;

                if (endDate <= hireDate)
                {
                    person.EndDateStyle = "background: rgba(255, 0, 0, 0.30);"; // Default color for invalid dates
                    continue;
                }

                var totalDays = (endDate - hireDate).TotalDays;
                var daysSinceStart = (today - hireDate).TotalDays;

                // Ensure daysSinceStart is within valid range
                if (daysSinceStart < 0) daysSinceStart = 0;

                var percentage = (daysSinceStart / totalDays) * 100;
                percentage = Math.Clamp(percentage, 0, 100); // Clamp percentage between 0 and 100

                string color = percentage < 75
                    ? "rgba(0, 128, 0, 0.30)" // Green
                    : (percentage < 90 ? "rgba(255, 255, 0, 0.30)" : "rgba(255, 0, 0, 0.30)"); // Yellow or Red

                // Ensure valid percentage formatting
                string formattedPercentage = percentage.ToString("F2", CultureInfo.InvariantCulture);

                person.EndDateStyle = $"background: linear-gradient(to right, {color} {formattedPercentage}%, transparent {formattedPercentage}%);";
            }
        }
    }
}
