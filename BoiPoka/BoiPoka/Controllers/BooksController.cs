using Microsoft.AspNetCore.Mvc;
using BoiPoka.Models;
using BoiPoka.ViewModels;
using BoiPoka.Services;
using Microsoft.AspNetCore.Authorization;

namespace BoiPoka.Controllers;
[Authorize(Roles = "Admin")]
public class BooksController : Controller
{
    private readonly IBookService _bookService;
    private readonly IWebHostEnvironment _webHost;

    public BooksController(IBookService bookService, IWebHostEnvironment webHost)
    {
        _bookService = bookService;
        _webHost = webHost;
    }

    [HttpGet]
    public async Task<IActionResult> Index()
    {
        var books = await _bookService.GetAllTsAsync<Books>();
        return View(books);
    }

    [HttpGet]
    public async Task<IActionResult> Create()
    {
        var categoryList = await _bookService.GetAllTsAsync<Category>();
        var viewModel = _bookService.GetCreateBookViewModel(categoryList);
        return View(viewModel);
    }

    [HttpPost]
    public async Task<IActionResult> Create(CreateBookViewModel viewBook, IFormFile file)
    {
        ModelState.Remove("CoverImage");
        if (file == null || file.Length == 0)
        {
            ModelState.AddModelError("CoverImage", "Image of Book cover is required!");
            return View(viewBook);
        }

        if (ModelState.IsValid)
        {
            await _bookService.CreateBookAsync(viewBook, file, _webHost.WebRootPath);
            return RedirectToAction("Index", "Home");
        }

        return View(viewBook);
    }

    [HttpGet]
    public IActionResult CreateCategory()
    {
        return View();
    }

    [HttpPost]
    public async Task<IActionResult> CreateCategory(Category category)
    {
        if (category == null)
        {
            return View(category);
        }
        await _bookService.CreateNewCategoryAsync(category);
        return RedirectToAction("Index", "Books");
    }

    [HttpGet]
    public async Task<IActionResult> Edit(int id)
    {
        var book = await _bookService.GetBookByIdAsync(id);
        if (book == null)
        {
            return NotFound();
        }
        if(book.Category == null)
        {
            var category = await _bookService.GetNullCategoryAsync("Uncategorized");
            book.Category = category;
        }
        var categoryList = await _bookService.GetAllTsAsync<Category>();
        var viewModel = _bookService.GetCreateBookViewModel(book, categoryList);
        return View(viewModel);
    }

    [HttpPost]
    public async Task<IActionResult> Edit(int id, CreateBookViewModel viewModel, IFormFile file)
    {
        if (id != viewModel.BookId)
        { 
            return BadRequest(); 
        }

        ModelState.Remove("file");

        if (!ModelState.IsValid)
        {
            return View(viewModel);
        }
        try
        {
            await _bookService.UpdateBookAsync(viewModel, file, _webHost.WebRootPath);
            return RedirectToAction("Index", "Books");
        }
        catch (Exception ex)
        {
            ModelState.AddModelError("", "Failed to update book.");
            ModelState.AddModelError("", ex.Message);
        }
        return View(viewModel);
    }

    [HttpPost]
    public async Task<IActionResult> Delete(int id)
    {
        await _bookService.DeleteBookAsync(id);
        return RedirectToAction("Index", "Books");
    }
    [AllowAnonymous]
    [HttpGet]
    public async Task<IActionResult> Details(int id)
    {
        if (id == 0) 
        {
            return BadRequest(); 
        }
        var book = await _bookService.GetBookByIdAsync(id);
        if (book == null)
        {
            return NotFound();
        }

        return View(book);
    }
}

