using DEP_Blazor_WASM.Entities.Enums;

namespace DEP_Blazor_WASM.Entities.ViewModels
{
    public class UserViewModel
    {
        public int UserId { get; set; }
        public string Name { get; set; } = string.Empty;
        public int? DepartmentId { get; set; }
        public string? DepartmentName { get; set; }
        public int? LocationId { get; set; }
        public string? LocationName { get; set; }
        public UserRole UserRole { get; set; }
    }
}
