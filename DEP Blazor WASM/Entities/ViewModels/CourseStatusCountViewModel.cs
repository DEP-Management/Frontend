namespace DEP_Blazor_WASM.Entities.ViewModels
{
    public class CourseStatusCountViewModel
    {
        public int StatusId { get; set; }
        public string CourseStatus { get; set; } = string.Empty;
        public int PersonCount { get; set; }
    }
}
