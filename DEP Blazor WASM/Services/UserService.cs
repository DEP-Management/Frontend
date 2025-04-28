using DEP_Blazor_WASM.Entities.Enums;
using DEP_Blazor_WASM.Entities.Models;
using DEP_Blazor_WASM.Entities.RequestModels;
using DEP_Blazor_WASM.Entities.ViewModels;
using DEP_Blazor_WASM.Services.Interfaces;
using System.Net.Http.Json;

namespace DEP_Blazor_WASM.Services
{
    public class UserService : IUserService
    {
        private readonly HttpClient _httpClient;

        public UserService(IHttpClientFactory factory)
        {
            _httpClient = factory.CreateClient("ServerApi");
        }

        public async Task<ApiResponse<T>> AddUserAsync<T>(AddUserRequest model)
        {
            try
            {
                var response = await _httpClient.PostAsJsonAsync("users", model);

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


        public async Task<List<User>> GetUsersAsync()
        {
            var response = await _httpClient.GetAsync("users");

            var users = new List<User>();
            if (response.IsSuccessStatusCode)
            {
                users = await response.Content.ReadFromJsonAsync<List<User>>() ?? new List<User>();
            }

            return users;
        }

        public async Task<ApiResponse<User>> GetUserByIdAsync(int id)
        {
            var response = await _httpClient.GetAsync($"users/{id}");

            // Deserialize the ApiResponse from the server, even in failure cases.
            var apiResponse = await response.Content.ReadFromJsonAsync<ApiResponse<User>>();

            // If the response body is null, create a fallback error ApiResponse.
            return apiResponse ?? new ApiResponse<User>
            {
                Success = false,
                Message = $"Request failed with status code {response.StatusCode}."
            };
        }

        public async Task<ApiResponse<UserDashboardViewModel>> GetUserDashboardByIdAsync(int id)
        {
            var response = await _httpClient.GetAsync($"users/dashboard/{id}");

            var apiResponse = new ApiResponse<UserDashboardViewModel>();
            if (response.IsSuccessStatusCode)
            {
                // Deserialize the ApiResponse from the server, even in failure cases.
                apiResponse = await response.Content.ReadFromJsonAsync<ApiResponse<UserDashboardViewModel>>();
            }

            // If the response body is null, create a fallback error ApiResponse.
            return apiResponse ?? new ApiResponse<UserDashboardViewModel>
            {
                Success = false,
                Message = $"Request failed with status code {response.StatusCode}."
            };
        }

        public async Task<List<UserViewModel>> GetUsersByUserRoleAsync(UserRole userRole)
        {
            var response = await _httpClient.GetAsync($"users/userrole?userRole={userRole}");

            var users = new List<UserViewModel>();
            if (response.IsSuccessStatusCode)
            {
                users = await response.Content.ReadFromJsonAsync<List<UserViewModel>>() ?? new List<UserViewModel>();
            }

            return users;
        }


        public async Task<ApiResponse<object>> ChangePasswordAsync(ChangePasswordRequest model)
        {
            try
            {
                var response = await _httpClient.PutAsJsonAsync("auth/changepassword", model);

                var apiResponse = await response.Content.ReadFromJsonAsync<ApiResponse<object>>();

                if (apiResponse is null)
                {
                    throw new Exception("Invalid response format.");
                }

                return apiResponse;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Exception in ChangePasswordAsync: {ex.Message}");
                return new ApiResponse<object>
                {
                    Success = false,
                    Message = "An unexpected error occurred."
                };
            }
        }

        public async Task<bool> ResetPasswordAsync(int userId)
        {
            var response = await _httpClient.PutAsync($"Auth/resetpassword/{userId}", null);

            if (response.IsSuccessStatusCode)
            {
                // Deserialize the response body to a bool
                return await response.Content.ReadFromJsonAsync<bool>();
            }

            return false;
        }

        public async Task<bool> UpdateUserAsync(User model)
        {
            var response = await _httpClient.PutAsJsonAsync("users", model);

            if (response.IsSuccessStatusCode)
            {
                // Deserialize the response body to a bool
                return await response.Content.ReadFromJsonAsync<bool>();
            }

            return false;
        }

        public async Task<bool> DeleteUserAsync(int userId)
        {
            var response = await _httpClient.DeleteAsync($"users/{userId}");

            if (response.IsSuccessStatusCode)
            {
                // Deserialize the response body to a bool
                return await response.Content.ReadFromJsonAsync<bool>();
            }

            return false;
        }

        public async Task<List<User>> GetUsersByUserRoleLageAsync(UserRole userRole)
        {
            var response = await _httpClient.GetAsync("users/userrole?userRole="+userRole);

            var users = new List<User>();
            if (response.IsSuccessStatusCode)
            {
                users = await response.Content.ReadFromJsonAsync<List<User>>() ?? new List<User>();
            }

            return users;
        }

    }
}
