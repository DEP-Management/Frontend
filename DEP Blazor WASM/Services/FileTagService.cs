using DEP_Blazor_WASM.Entities.Models;
using DEP_Blazor_WASM.Entities.ViewModels;
using DEP_Blazor_WASM.Pages;
using DEP_Blazor_WASM.Services.Interfaces;
using System.Net.Http.Json;

namespace DEP_Blazor_WASM.Services
{
    public class FileTagService : IFileTagService
    {
        private readonly HttpClient _httpClient;

        public FileTagService(IHttpClientFactory factory)
        {
            _httpClient = factory.CreateClient("ServerApi");
        }

        public async Task<bool> AddFileTagAsync(FileTagViewModel fileTag)
        {
            var response = await _httpClient.PostAsJsonAsync("FileTag", fileTag);

            if (response.IsSuccessStatusCode)
            {
                // Deserialize the response body to a bool
                return await response.Content.ReadFromJsonAsync<bool>();
            }

            return false; // Indicate failure if the request was not successful
        }

        public async Task<List<FileTagViewModel>> GetAllFileTagsAsync()
        {
            var response = await _httpClient.GetAsync($"FileTag");

            var fileTags = new List<FileTagViewModel>();

            if (response.IsSuccessStatusCode)
            {
                fileTags = await response.Content.ReadFromJsonAsync<List<FileTagViewModel>>() ?? new List<FileTagViewModel>();
            }

            return fileTags;
        }

        public async Task<bool> UpdateFileTagAsync(FileTagViewModel model)
        {
            var response = await _httpClient.PutAsJsonAsync("FileTag", model);

            if (response.IsSuccessStatusCode)
            {
                // Deserialize the response body to a bool
                return await response.Content.ReadFromJsonAsync<bool>();
            }

            return false;
        }

        public async Task<bool> DeleteFileTagAsync(int fileTagId)
        {
            var response = await _httpClient.DeleteAsync($"FileTag/{fileTagId}");

            if (response.IsSuccessStatusCode)
            {
                // Deserialize the response body to a bool
                return await response.Content.ReadFromJsonAsync<bool>();
            }

            return false;
        }
    }
}
