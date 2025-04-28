using DEP_Blazor_WASM.Entities.Models;
using DEP_Blazor_WASM.Entities.ViewModels;

namespace DEP_Blazor_WASM.Services.Interfaces
{
    public interface IFileTagService
    {
        Task<bool> AddFileTagAsync(FileTagViewModel fileTag);
        Task<List<FileTagViewModel>> GetAllFileTagsAsync();
        Task<bool> UpdateFileTagAsync(FileTagViewModel model);
        Task<bool> DeleteFileTagAsync(int fileTagId);
    }
}
