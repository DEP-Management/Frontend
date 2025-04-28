using DEP_Blazor_WASM.Entities.Models;

namespace DEP_Blazor_WASM.Services.Interfaces
{
    public interface ICourseService
    {
        Task<bool> AddCourseAsync(Course course);
        Task<List<Course>> GetCoursesAsync();
        Task<List<Course>> GetCoursesByModuleAsync(int id);
        Task<bool> UpdateCourseAsync(Course model);
        Task<bool> DeleteCourseAsync(int bookId);
    }
}
