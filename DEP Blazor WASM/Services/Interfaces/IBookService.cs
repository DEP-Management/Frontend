using DEP_Blazor_WASM.Entities.Models;

namespace DEP_Blazor_WASM.Services.Interfaces
{
    public interface IBookService
    {
        Task<bool> AddBookAsync(Book book);
        Task<List<Book>> GetBooksAsync();
        Task<bool> UpdateBookAsync(Book model);
        Task<bool> DeleteBookAsync(int bookId);
    }
}
