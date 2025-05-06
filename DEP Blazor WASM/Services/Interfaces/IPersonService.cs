using DEP_Blazor_WASM.Entities.Models;
using DEP_Blazor_WASM.Entities.RequestModels;

namespace DEP_Blazor_WASM.Services.Interfaces
{
    public interface IPersonService
    {
        Task<ApiResponse<T>> AddPersonAsync<T>(Person person);
        Task<List<Person>> GetPersonsAsync();
        Task<List<Person>> GetPersonExcelAsync(int id);
        Task<List<Person>> GetPersonsByCourseIdAsync(int courseId);
        Task<Person?> GetPersonByIdAsync(int personId);
        Task<bool> UpdatePersonAsync(Person model);
        Task<bool> DeletePersonAsync(int personId);
        void ComputeEndDateStyles<T>(List<T> persons) where T : IHasEndDate;
    }
}
