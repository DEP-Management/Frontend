using DEP_Blazor_WASM.Entities.Enums;
using System.Text.Json.Serialization;

namespace DEP_Blazor_WASM.Entities.Models
{
    public class User
    {
        public int UserId { get; set; }
        public int? DepartmentId { get; set; }
        public int? LocationId { get; set; }
        public int? EducationBossId { get; set; } // Uddannelseschef
        public string Name { get; set; } = string.Empty;
        public string UserName { get; set; } = string.Empty;
        public UserRole UserRole { get; set; }
        public DateTime PasswordExpiryDate { get; set; }

        public Department? Department { get; set; }
        public Location? Location { get; set; }
        public User? EducationBoss { get; set; } // Uddannelseschef
        public List<User> EducationLeaders { get; set; } = new List<User>(); // Uddannelsesledere i.e dem som brugeren er chef for.
        public List<Person> EducationalConsultantPersons { get; set; } = new List<Person>(); // Undervisere som brugeren er pædagogisk konsulent for
        public List<Person> EducationLeaderPersons { get; set; } = new List<Person>(); // Undervisere som brugeren er uddannelsesleder for
        public List<Person> OperationCoordinatorPersons { get; set; } = new List<Person>(); // Undervisere som brugeren er driftskoordinator for


        // Used for MudSelects to fix issues displaying names for selected value.
        public override string ToString()
        {
            return Name;
        }
    }
}
