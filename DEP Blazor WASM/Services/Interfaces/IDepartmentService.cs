using DEP_Blazor_WASM.Entities.Models;

namespace DEP_Blazor_WASM.Services.Interfaces
{
    public interface IDepartmentService
    {
        Task<bool> AddDepartmentAsync(Department department);
        Task<List<Department>> GetDepartmentsAsync();
        Task<bool> UpdateDepartmentAsync(Department model);
        Task<bool> DeleteDepartmentAsync(int departmentId);
    }
}
