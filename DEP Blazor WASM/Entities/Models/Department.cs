namespace DEP_Blazor_WASM.Entities.Models
{
    public class Department
    {
        public int DepartmentId { get; set; }
        public string Name { get; set; } = string.Empty;

        
        // Used for MudSelects to fix issues displaying names for selected value.
        public override string ToString()
        {
            return Name;
        }
    }
}
