using DEP_Blazor_WASM.Entities.Enums;

namespace DEP_Blazor_WASM.Entities.RequestModels
{
    public class AddUserRequest
    {
        public string Username { get; set; } = string.Empty;
        public string Name { get; set; } = string.Empty;
        public int? EducationBossId { get; set; }
        public int? LocationId { get; set; }
        public int? DepartmentId { get; set; }
        public UserRole UserRole { get; set; }
    }
}
