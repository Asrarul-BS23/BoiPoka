using BoiPoka.Repositories;
using BoiPoka.Models;
using Microsoft.AspNetCore.Mvc.Rendering;
using BoiPoka.ViewModels;

namespace BoiPoka.Services;

public class HomeService : IHomeService
{
    private readonly IHomeRepository _homeRepository;
    private readonly IRepository _repository;
    public HomeService(IHomeRepository homeRepository, IRepository repository)
    {
        _homeRepository = homeRepository;
        _repository = repository;
    }
    public async Task<IEnumerable<SelectListItem>> GetCategoryListAsync()
    {
        return await _homeRepository.GetCategoryListAsync();
    }
    public async Task<IEnumerable<Books>> GetAllBookAsync()
    {
        return await _repository.GetAllTsAsync<Books>();
    }
    public async Task<BookStoreViewModel> FilterAndSearchAsync(BookStoreViewModel bookStoreViewModel)
    {
        IEnumerable<Books> books;
        if (bookStoreViewModel.SelectedCategoryName != null && bookStoreViewModel.SearchQuery != null)
        {
            books = await _homeRepository.FilterAndSearchAsync(bookStoreViewModel);
        }
        else if (bookStoreViewModel.SelectedCategoryName != null)
        {
            books = await _homeRepository.FilterByCategoryAsync(bookStoreViewModel);
        }
        else
        {
            books = await _homeRepository.FilterBySearchAsync(bookStoreViewModel);
        }
        var categoryList = await _homeRepository.GetCategoryListAsync();
        var newStoreViewModel = new BookStoreViewModel
        {
            Books = books,
            CategoryList = categoryList,
        };
        return newStoreViewModel;
    }
}
