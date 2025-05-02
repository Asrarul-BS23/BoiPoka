using BoiPoka.Repositories;
using BoiPoka.Models;
using Microsoft.AspNetCore.Mvc.Rendering;
using BoiPoka.ViewModels;

namespace BoiPoka.Services;

public class HomeService : IHomeService
{
    private readonly IHomeRepository _repository;
    public HomeService(IHomeRepository repository)
    {
        _repository = repository;
    }
    public async Task<IEnumerable<SelectListItem>> GetCategoryListAsync()
    {
        return await _repository.GetCategoryListAsync();
    }
    public async Task<IEnumerable<Books>> GetAllBookAsync()
    {
        return await _repository.GetAllBooksAsync();
    }
    public async Task<BookStoreViewModel> FilterAndSearchAsync(BookStoreViewModel bookStoreViewModel)
    {
        IEnumerable<Books> books;
        if (bookStoreViewModel.SelectedCategoryName != null && bookStoreViewModel.SearchQuery != null)
            books = await _repository.FilterAndSearchAsync(bookStoreViewModel);
        else if (bookStoreViewModel.SelectedCategoryName != null)
            books = await _repository.FilterByCategoryAsync(bookStoreViewModel);
        else
            books = await _repository.FilterBySearchAsync(bookStoreViewModel);
        var categoryList = await _repository.GetCategoryListAsync();
        var newStoreViewModel = new BookStoreViewModel
        {
            Books = books,
            CategoryList = categoryList,
        };
        return newStoreViewModel;
    }
}
