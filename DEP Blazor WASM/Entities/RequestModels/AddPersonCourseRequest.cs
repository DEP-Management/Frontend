using DEP_Blazor_WASM.Entities.Enums;

namespace DEP_Blazor_WASM.Entities.RequestModels
{
    public class AddPersonCourseRequest
    {
        public int PersonId { get; set; }
        public int CourseId { get; set; }
        public Status Status { get; set; }
    }
}
