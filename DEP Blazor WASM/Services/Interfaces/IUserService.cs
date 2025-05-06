using DEP_Blazor_WASM.Entities.Enums;
using DEP_Blazor_WASM.Entities.Models;
using DEP_Blazor_WASM.Entities.RequestModels;
using DEP_Blazor_WASM.Entities.ViewModels;

namespace DEP_Blazor_WASM.Services.Interfaces
{
    public interface IUserService
    {
        Task<ApiResponse<T>> AddUserAsync<T>(AddUserRequest model);
        Task<List<User>> GetUsersAsync();
        Task<List<User>> GetUsersByUserRoleLageAsync(UserRole userRole);
        Task<ApiResponse<User>> GetUserByIdAsync(int id);
        Task<ApiResponse<UserDashboardViewModel>> GetUserDashboardByIdAsync(int id);
        Task<List<UserViewModel>> GetUsersByUserRoleAsync(UserRole userRole);
        Task<ApiResponse<object>> ChangePasswordAsync(ChangePasswordRequest model);
        Task<bool> ResetPasswordAsync(int userId);
        Task<bool> UpdateUserAsync(User model);
        Task<bool> DeleteUserAsync(int userId);
        Task<List<User>> GetUsersByEducationBossId(int id);
        Task<List<EducationalLeaderViewModel>> GetEducationalLeaderAsync(int id);
        Task<List<EducationalBossViewModel>> GetEducationalBossesAsync();
        Task<List<EducationalBossViewModel>> GetEducationalBossAsync(int id);
    }
}
