using DEP_Blazor_WASM.Services.Interfaces;

namespace DEP_Blazor_WASM.Entities.Models
{
    public class Person : IHasEndDate // Person == Underviser
    {
        public int PersonId { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Initials { get; set; } = string.Empty;
        public int? DepartmentId { get; set; }
        public int? LocationId { get; set; }
        public int? EducationalConsultantId { get; set; }
        public int? EducationalLeaderId { get; set; }
        public int? OperationCoordinatorId { get; set; }
        public DateTime HiringDate { get; set; }
        public DateTime EndDate { get; set; }
        public bool SvuEligible { get; set; }
        public bool SvuApplied { get; set; }

        public User? EducationalConsultant { get; set; }
        public User? EducationalLeader { get; set; }
        public User? OperationCoordinator { get; set; }
        public Department? Department { get; set; }
        public Location? Location { get; set; }
        public List<File> Files { get; set; } = new List<File>();
        public List<PersonCourse> PersonCourses { get; set; } = new List<PersonCourse>();


        // Used for displaying in the table in Teachers.razor
        public int CompletedModules { get; set; }
        public string EndDateStyle { get; set; } = string.Empty;

    }
}
