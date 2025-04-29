using BoiPoka.ViewModels;
using BoiPoka.Models;

namespace BoiPoka.Services;

public interface IBookService
{
    Task<List<Books>> GetAllBooksAsync();
    Task<Books?> GetBookByIdAsync(int id);
    Task CreateBookAsync(CreateBookViewModel viewModel, IFormFile file, string rootPath);
    Task UpdateBookAsync(CreateBookViewModel viewModel, IFormFile file, string rootPath);
    Task<Category> GetNullCategoryAsync(string categoryName);
    Task DeleteBookAsync(int id);
    Task<IEnumerable<Category>> GetCategoriesAsync();
    Task CreateNewCategoryAsync(Category category);
}
