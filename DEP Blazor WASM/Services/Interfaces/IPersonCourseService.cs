using DEP_Blazor_WASM.Entities.Models;

namespace DEP_Blazor_WASM.Services.Interfaces
{
    public interface IPersonCourseService
    {
        Task<List<PersonCourse>> GetAllPersonCoursesForPersonAsync(int personId);
        Task<bool> AddPersonCourseAsync(PersonCourse personCourse);
        Task<bool> UpdatePersonCourseAsync(PersonCourse updatedPersonCourse);
        Task<bool> DeletePersonCourseAsync(int personId, int courseId);
    }
}
