using Microsoft.AspNetCore.Mvc.Rendering;
using BoiPoka.Models;
using BoiPoka.ViewModels;
namespace BoiPoka.Repositories;

public interface IHomeRepository
{
    Task<IEnumerable<SelectListItem>> GetCategoryListAsync();
    Task<IEnumerable<Books>> GetAllBooksAsync();
    Task<IEnumerable<Books>> FilterAndSearchAsync(BookStoreViewModel bookStoreViewModel);
    Task<IEnumerable<Books>> FilterByCategoryAsync(BookStoreViewModel bookStoreViewModel);
    Task<IEnumerable<Books>> FilterBySearchAsync(BookStoreViewModel bookStoreViewModel);
}
