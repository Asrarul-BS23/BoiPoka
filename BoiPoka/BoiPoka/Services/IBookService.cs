using BoiPoka.ViewModels;
using BoiPoka.Models;

namespace BoiPoka.Services;

public interface IBookService
{
    Task<IEnumerable<T>> GetAllTsAsync<T>() where T : class;
    Task<Books?> GetBookByIdAsync(int id);
    Task CreateBookAsync(CreateBookViewModel viewModel, IFormFile file, string rootPath);
    Task UpdateBookAsync(CreateBookViewModel viewModel, IFormFile file, string rootPath);
    Task<Category> GetNullCategoryAsync(string categoryName);
    Task DeleteBookAsync(int id);
    Task CreateNewCategoryAsync(Category category);
}
