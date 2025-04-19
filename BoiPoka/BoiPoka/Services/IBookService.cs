using BoiPoka.ViewModels;
using BoiPoka.Models;

namespace BoiPoka.Services;

public interface IBookService
{
    Task<List<Books>> GetAllBooksAsync();
    Task<Books?> GetBookByIdAsync(int id);
    Task CreateBookAsync(CreateBookViewModel viewModel, IFormFile file, string rootPath);
    Task UpdateBookAsync(Books book);
    Task DeleteBookAsync(int id);
}
