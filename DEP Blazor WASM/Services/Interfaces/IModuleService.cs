using DEP_Blazor_WASM.Entities.Models;
using DEP_Blazor_WASM.Entities.RequestModels;
using DEP_Blazor_WASM.Entities.ViewModels;

namespace DEP_Blazor_WASM.Services.Interfaces
{
    public interface IModuleService
    {
        Task<List<Module>> GetModulesAsync();
        Task<List<ModuleWithCourseViewModel>> GetModulesWithCoursesAsync();
        Task<List<ModuleWithCourseViewModel>> GetModuleWithCoursesAsync(int id);
        Task<bool> AddModuleAsync(Module module);
        Task<bool> UpdateModuleAsync(Module model);
        Task<bool> DeleteModuleAsync(int id);
    }
}
