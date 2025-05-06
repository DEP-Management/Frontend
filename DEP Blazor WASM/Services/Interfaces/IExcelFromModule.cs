using DEP_Blazor_WASM.Entities.Models;
using DEP_Blazor_WASM.Entities.ViewModels;

namespace DEP_Blazor_WASM.Services.Interfaces
{
    public interface IExcelFromModule
    {
        Task<byte[]> GenerateModuleExcelAsync(List<ModuleWithCourseViewModel> modules);
        Task<byte[]> GenerateCourseExcelAsync(List<Course> courses);
    }
}
