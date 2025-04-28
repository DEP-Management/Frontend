using DEP_Blazor_WASM.Entities.Models;
using DEP_Blazor_WASM.Services.Interfaces;
using Microsoft.JSInterop;
using System.Net.Http.Json;
using System.Security.Claims;
using System.Text.Json;
using File = DEP_Blazor_WASM.Entities.Models.File;

namespace DEP_Blazor_WASM.Services
{
    public class FileService : IFileService
    {
        private readonly HttpClient _httpClient;
        private readonly IJSRuntime _jsRuntime;
        private readonly CustomAuthStateProvider _authStateProvider;

        public FileService(IHttpClientFactory factory, IJSRuntime jsRuntime, CustomAuthStateProvider authStateProvider)
        {
            _httpClient = factory.CreateClient("ServerApi");
            _jsRuntime = jsRuntime;
            _authStateProvider = authStateProvider;
        }

        public async Task<List<File>> GetAllFilesAsync()
        {
            // Get the authentication state
            var authState = await _authStateProvider.GetAuthenticationStateAsync();
            var user = authState.User;

            // Extract the roleId from claims
            var roleIdClaim = user.FindFirst(c => c.Type == "roleId")?.Value;

            if (roleIdClaim == null || !int.TryParse(roleIdClaim, out int roleId))
            {
                return new List<File>();  // roleId claim not found or invalid format
            }

            var response = await _httpClient.GetAsync($"File/role/{roleId}");

            var files = new List<File>();
            if (response.IsSuccessStatusCode)
            {
                files = await response.Content.ReadFromJsonAsync<List<File>>() ?? new List<File>();
            }

            return files;
        }

        public async Task DownloadFileAsync(int fileId)
        {
            // Call the API endpoint
            var response = await _httpClient.GetAsync($"File/DownloadFile/{fileId}");

            if (response.IsSuccessStatusCode)
            {
                var fileName = response.Content.Headers.ContentDisposition?.FileName?.Trim('"')
                               ?? "downloaded-file";
                var fileContent = await response.Content.ReadAsByteArrayAsync();

                // Trigger the download using JavaScript
                var fileBase64 = Convert.ToBase64String(fileContent);
                var contentType = response.Content.Headers.ContentType?.ToString()
                                  ?? "application/octet-stream";

                await _jsRuntime.InvokeVoidAsync("downloadFile", fileName, fileBase64, contentType);
            }
            else
            {
                throw new Exception($"Failed to download file: {response.ReasonPhrase}");
            }
        }

        public async Task<List<File>> UploadFilesAsync(MultipartFormDataContent formData)
        {
            try
            {
                // Make the POST request to upload the files.
                var response = await _httpClient.PostAsync($"File/multiple", formData);

                if (response.IsSuccessStatusCode)
                {
                    var jsonResponse = await response.Content.ReadAsStringAsync();

                    // Configure JsonSerializerOptions to ignore casing
                    var options = new JsonSerializerOptions
                    {
                        PropertyNameCaseInsensitive = true
                    };

                    // Deserialize the JSON into a list of File objects
                    var uploadedFiles = JsonSerializer.Deserialize<List<File>>(jsonResponse, options);

                    return uploadedFiles;
                }

                return null;
            }
            catch (Exception ex)
            {
                // Handle any errors (e.g., network issues)
                Console.WriteLine($"Error uploading File(s): {ex.Message}");
                return null;
            }
        }

        public async Task<bool> DeleteFileAsync(Entities.Models.File file)
        {
            try
            {
                // Make the DELETE request to delete the File
                var response = await _httpClient.DeleteAsync($"File/{file.FileId}");

                // Return true if the delete was successful, otherwise false
                return response.IsSuccessStatusCode;
            }
            catch (Exception ex)
            {
                // Handle any errors (e.g., network issues)
                Console.WriteLine($"Error deleting File: {ex.Message}");
                return false;
            }
        }
    }
}
