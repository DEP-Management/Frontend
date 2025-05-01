using DEP_Blazor_WASM.Entities.Enums;

namespace DEP_Blazor_WASM.Entities.ViewModels
{
    public class EducationalBossViewModel
    {
        public int UserId { get; set; }
        public string Name { get; set; }

        public UserRole UserRole { get; set; }

        public List<EducationalLeaderViewModel> EducationalLeaders { get; set; } = new List<EducationalLeaderViewModel>();
    }
}
