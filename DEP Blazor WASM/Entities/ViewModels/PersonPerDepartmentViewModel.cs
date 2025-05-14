namespace DEP_Blazor_WASM.Entities.ViewModels
{
    public class PersonPerDepartmentViewModel
    {
        public int DepartmentId { get; set; }
        public string DepartmentName { get; set; } = string.Empty;
        public int TeacherCount { get; set; }
    }
}
