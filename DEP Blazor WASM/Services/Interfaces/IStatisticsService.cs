using DEP_Blazor_WASM.Entities.ViewModels;

namespace DEP_Blazor_WASM.Services.Interfaces
{
    public interface IStatisticsService
    {
        Task<List<PersonPerDepartmentViewModel>> GetPersonsPerDepartmentAsync();
        Task<List<PersonPerDepartmentViewModel>> GetPersonsPerDepartmentByModuleIdAsync(int moduleId);
        Task<List<PersonPerLocationViewModel>> GetPersonsPerLocationAsync();
        Task<List<CourseStatusCountViewModel>> GetPersonsPerCourseStatusByModuleIdAsync(int moduleId);
    }
}
