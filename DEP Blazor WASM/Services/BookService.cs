using DEP_Blazor_WASM.Entities.Models;
using DEP_Blazor_WASM.Services.Interfaces;
using System.Net.Http.Json;

namespace DEP_Blazor_WASM.Services
{
    public class BookService : IBookService
    {
        private readonly HttpClient _httpClient;

        public BookService(IHttpClientFactory factory)
        {
            _httpClient = factory.CreateClient("ServerApi");
        }

        public async Task<bool> AddBookAsync(Book book)
        {
            var response = await _httpClient.PostAsJsonAsync("book", book);

            if (response.IsSuccessStatusCode)
            {
                // Deserialize the response body to a bool
                return await response.Content.ReadFromJsonAsync<bool>();
            }

            return false; // Indicate failure if the request was not successful
        }

        public async Task<List<Book>> GetBooksAsync()
        {
            var response = await _httpClient.GetAsync("book");

            var books = new List<Book>();
            if (response.IsSuccessStatusCode)
            {
                books = await response.Content.ReadFromJsonAsync<List<Book>>() ?? new List<Book>();
            }

            return books;
        }

        public async Task<bool> UpdateBookAsync(Book model)
        {
            var response = await _httpClient.PutAsJsonAsync("book", model);

            if (response.IsSuccessStatusCode)
            {
                // Deserialize the response body to a bool
                return await response.Content.ReadFromJsonAsync<bool>();
            }

            return false;
        }

        public async Task<bool> DeleteBookAsync(int bookId)
        {
            var response = await _httpClient.DeleteAsync($"book/{bookId}");

            if (response.IsSuccessStatusCode)
            {
                // Deserialize the response body to a bool
                return await response.Content.ReadFromJsonAsync<bool>();
            }

            return false;
        }
    }
}
