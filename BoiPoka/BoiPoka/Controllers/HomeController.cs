using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using BoiPoka.Models;
using BoiPoka.Data;
using BoiPoka.ViewModels;
using BoiPoka.Services;

namespace BoiPoka.Controllers;

public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;
    private readonly IHomeService _homeService;
    private readonly AppDbContext _context;

    public HomeController(ILogger<HomeController> logger, AppDbContext context, IHomeService homeService)
    {
        _logger = logger;
        _context = context;
        _homeService = homeService;
    }
    
    public async Task<IActionResult> Index()
    {
        var allBooks = await _homeService.GetAllBookAsync();
        var categoryList = await _homeService.GetCategoryListAsync();
        var bookListForHomePage = new BookStoreViewModel { 
            Books = allBooks,
            CategoryList = categoryList,
        };
        return View(bookListForHomePage);
    }
    [HttpPost]
    public async Task<IActionResult> Index(BookStoreViewModel bookStoreViewModel)
    {
        if(bookStoreViewModel.SearchQuery!=null || bookStoreViewModel.SelectedCategoryName != null)
        {
            var newStoreViewModel = await _homeService.FilterAndSearchAsync(bookStoreViewModel);
            return View(newStoreViewModel);
        }
        return RedirectToAction("Index", "Home"); ;
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}