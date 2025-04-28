using DEP_Blazor_WASM.Entities.Enums;
using DEP_Blazor_WASM.Entities.RequestModels;

namespace DEP_Blazor_WASM.Services.Interfaces
{
    public interface IAuthService
    {
        ValueTask<string?> GetJwtAsync();
        Task<bool> LoginAsync(LoginRequest loginModel);
        Task LogoutAsync();
        Task<bool> RefreshAsync();
        Task<int?> GetCurrentUserId();
        Task<UserRole?> GetCurrentUserRole();
    }
}
