using DEP_Blazor_WASM.Entities.Models;
using DEP_Blazor_WASM.Entities.ViewModels;

namespace DEP_Blazor_WASM.Services.Interfaces
{
    public interface IExcelFromBoss
    {
        Task<byte[]> GenerateBossExcelAsync(List<EducationalBossViewModel> bosses);
        Task<byte[]> GenerateLeaderExcelAsync(List<EducationalLeaderViewModel> leaders);
        Task<byte[]> GenerateTeacherExcelAsync(List<Person> teachers);
    }
}
