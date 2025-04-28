using Microsoft.AspNetCore.Mvc;
using BoiPoka.Models;
using BoiPoka.ViewModels;
using BoiPoka.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace BoiPoka.Controllers;
[Authorize]
public class BooksController : Controller
{
    private readonly IBookService _bookService;
    private readonly IWebHostEnvironment _webHost;

    public BooksController(IBookService bookService, IWebHostEnvironment webHost)
    {
        _bookService = bookService;
        _webHost = webHost;
    }

    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> Index()
    {
        var books = await _bookService.GetAllBooksAsync();
        return View(books);
    }

    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> Create()
    {
        var categoryList = await _bookService.GetCategoriesAsync();
        foreach(var category in categoryList)
        {
            Console.WriteLine(category.Name);
        }
        return View(new CreateBookViewModel
        {
            CategoryList = categoryList.Select(c => new SelectListItem
            {
                Value = c.Name,
                Text = c.Name
            }).ToList()
        });
    }

    [Authorize(Roles = "Admin")]
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


    public IActionResult CreateCategory()
    {
        return View();
    }
    [Authorize(Roles = "Admin")]
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

    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> Edit(int id)
    {
        var book = await _bookService.GetBookByIdAsync(id);
        if (book == null) return NotFound();
        if(book.Category == null)
        {
            var category = await _bookService.GetNullCategoryAsync("Uncategorized");
            book.Category = category;
        }
        var categoryList = await _bookService.GetCategoriesAsync();
        var viewModel = new CreateBookViewModel
        {
            BookId = book.BookId,
            Title = book.Title,
            Category = book.Category.Name ?? "Uncategorized",
            Description = book.Description,
            Author = book.Author,
            Price = book.Price,
            StockQuantity = book.StockQuantity,
            CoverImage = book.CoverImage,
            CreatedAt = book.CreatedAt,
            CategoryList = categoryList.Select(c => new SelectListItem
            {
                Value = c.Name,
                Text = c.Name
            }).ToList()
        };
        return View(viewModel);
    }


    [Authorize(Roles = "Admin")]
    [HttpPost]
    public async Task<IActionResult> Edit(int id, CreateBookViewModel viewModel, IFormFile file)
    {
        Console.WriteLine($"{id}==>{viewModel.BookId}");
        if (id != viewModel.BookId) return BadRequest();

        ModelState.Remove("file");

        if (!ModelState.IsValid)
            return View(viewModel);
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

    [Authorize(Roles = "Admin")]
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

