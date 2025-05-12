using BoiPoka.Models;
using BoiPoka.ViewModels;

namespace BoiPoka.Repositories;

public interface IBookRepository
{
    Task<Books?> GetByIdAsync(int id);
    Task<Category> GetBookCategory(string categoryName);
}
