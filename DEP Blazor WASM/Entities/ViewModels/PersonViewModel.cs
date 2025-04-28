using DEP_Blazor_WASM.Entities.Models;
using DEP_Blazor_WASM.Services.Interfaces;

namespace DEP_Blazor_WASM.Entities.ViewModels
{
    public class PersonViewModel : IHasEndDate
    {
        public int PersonId { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Initials { get; set; } = string.Empty;
        public DateTime HiringDate { get; set; }
        public DateTime EndDate { get; set; }
        public bool SvuEligible { get; set; }
        public bool SvuApplied { get; set; }
        public int? DepartmentId { get; set; }
        public string? DepartmentName { get; set; }
        public int? LocationId { get; set; }
        public string? LocationName { get; set; }
        public int CompletedModules { get; set; }
        public string EndDateStyle { get; set; } = string.Empty;
        public List<PersonCourse> PersonCourses { get; set; } = new List<PersonCourse>();

    }
}
