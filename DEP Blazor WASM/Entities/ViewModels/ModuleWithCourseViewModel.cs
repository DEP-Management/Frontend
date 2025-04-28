using DEP_Blazor_WASM.Entities.Models;

namespace DEP_Blazor_WASM.Entities.ViewModels
{
    public class ModuleWithCourseViewModel
    {
        public int ModuleId { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;

        public List<Course> Courses { get; set; } = new List<Course>();
    }
}
