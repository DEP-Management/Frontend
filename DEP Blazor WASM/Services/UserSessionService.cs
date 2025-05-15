using DEP_Blazor_WASM.Entities.Enums;

namespace DEP_Blazor_WASM.Services
{
    public class UserSessionService
    {
        public UserRole? CurrentUserRole { get; set; }
        public string? UserId { get; set; }
        public string? UserName { get; set; }

        public bool IsAuthenticated => CurrentUserRole != null;
    }

}
