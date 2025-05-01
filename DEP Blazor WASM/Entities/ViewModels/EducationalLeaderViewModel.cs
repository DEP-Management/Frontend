using DEP_Blazor_WASM.Entities.Enums;
using DEP_Blazor_WASM.Entities.Models;

namespace DEP_Blazor_WASM.Entities.ViewModels
{
    public class EducationalLeaderViewModel
    {
        public int UserId { get; set; }
        public string Name { get; set; } = string.Empty;
        public int? LocationId { get; set; }
        public int? DepartmentId { get; set; }
        public UserRole UserRole { get; set; }
        public int? EducationBossId { get; set; } //Uddannelseschef

        public Department? Department { get; set; }
        public Location? Location { get; set; }
        public List<Person> Teachers { get; set; } = new List<Person>();
    }
}
