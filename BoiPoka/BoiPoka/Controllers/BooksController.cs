using Microsoft.AspNetCore.Mvc;
using BoiPoka.Models;
using BoiPoka.ViewModels;
using BoiPoka.Services;

namespace BoiPoka.Controllers;

public class BooksController : Controller
{
    private readonly IBookService _bookService;
    private readonly IWebHostEnvironment _webHost;

    public BooksController(IBookService bookService, IWebHostEnvironment webHost)
    {
        _bookService = bookService;
        _webHost = webHost;
    }

    public async Task<IActionResult> Index()
    {
        var books = await _bookService.GetAllBooksAsync();
        return View(books);
    }

    public IActionResult Create() => View();

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

    public async Task<IActionResult> Edit(int id)
    {
        var book = await _bookService.GetBookByIdAsync(id);
        if (book == null) return NotFound();
        return View(book);
    }

    [HttpPost]
    public async Task<IActionResult> Edit(int id, Books model)
    {
        if (id != model.BookId) return BadRequest();

        if (ModelState.IsValid)
        {
            try
            {
                await _bookService.UpdateBookAsync(model);
                return RedirectToAction("Index", "Books");
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", "Failed to update book.");
                ModelState.AddModelError("", ex.Message);
            }
        }

        return View(model);
    }

    [HttpPost]
    public async Task<IActionResult> Delete(int id)
    {
        await _bookService.DeleteBookAsync(id);
        return RedirectToAction("Index", "Books");
    }

    [HttpGet]
    public async Task<IActionResult> Details(int id)
    {
        if (id == 0) return BadRequest();

        var book = await _bookService.GetBookByIdAsync(id);
        if (book == null) return NotFound();

        return View(book);
    }
}

