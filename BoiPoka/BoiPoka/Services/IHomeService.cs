using Microsoft.AspNetCore.Mvc.Rendering;
using BoiPoka.Models;
using BoiPoka.ViewModels;
namespace BoiPoka.Services;

public interface IHomeService
{
    Task<IEnumerable<SelectListItem>> GetCategoryListAsync();
    Task<IEnumerable<Books>> GetAllBookAsync();
    Task<BookStoreViewModel> FilterAndSearchAsync(BookStoreViewModel bookStoreViewModel);
}
