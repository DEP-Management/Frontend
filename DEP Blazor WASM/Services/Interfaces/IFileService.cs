using File = DEP_Blazor_WASM.Entities.Models.File;

namespace DEP_Blazor_WASM.Services.Interfaces
{
    public interface IFileService
    {
        Task<List<File>> GetAllFilesAsync();
        Task DownloadFileAsync(int fileId);
        Task<List<File>> UploadFilesAsync(MultipartFormDataContent formData);
        Task<bool> DeleteFileAsync(File file);
    }
}
