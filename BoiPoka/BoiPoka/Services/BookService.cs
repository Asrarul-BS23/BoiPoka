using BoiPoka.ViewModels;
using BoiPoka.Models;
using BoiPoka.Repositories;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace BoiPoka.Services;

public class BookService : IBookService
{
    private readonly IBookRepository _bookRepository;
    private readonly IRepository _repository;

    public BookService(IBookRepository bookRepository, IRepository repository)
    {
        _bookRepository = bookRepository;
        _repository = repository;
    }

    public async Task<IEnumerable<T>> GetAllTsAsync<T>() where T : class
    {
        return await _repository.GetAllTsAsync<T>();
    }
    public async Task<Books?> GetBookByIdAsync(int id)
    {
        return await _repository.FindByIdAsync<Books>(id);
    }

    public async Task CreateNewCategoryAsync(Category category)
    {
        await _repository.AddAsync<Category>(category);
        await _repository.SaveChangesAsync();
    }

    public Books CreateNewBook(CreateBookViewModel viewModel, Category category)
    {
        var book = new Books();
        UpdateBookInformation(book, viewModel, category);
        return book;
    }
    public async Task CreateBookAsync(CreateBookViewModel viewModel, IFormFile file, string rootPath)
    {
        SaveImageToFile(file, rootPath);

        viewModel.CoverImage = "/uploads/" + file.FileName;

        var category = await _bookRepository.GetBookCategory(viewModel.Category);

        var book = CreateNewBook(viewModel, category); 

        await _repository.AddAsync<Books>(book);
        await _repository.SaveChangesAsync();
    }
    public CreateBookViewModel GetCreateBookViewModel(IEnumerable<Category> categoryList)
    {
        return GetCreateBookViewModel(new Books(), categoryList);
    }
    public CreateBookViewModel GetCreateBookViewModel(Books book, IEnumerable<Category> categoryList)
    {
        var viewModel = new CreateBookViewModel
        {
            BookId = book.BookId,
            Title = book.Title,
            Category = book.Category?.Name ?? "--Select a Category--",
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
        return viewModel;
    }

    public async void SaveImageToFile(IFormFile file, string rootPath)
    {
        string uploadFolder = Path.Combine(rootPath, "uploads");
        if (!Directory.Exists(uploadFolder))
        {
            Directory.CreateDirectory(uploadFolder);
        }
        string fileName = Path.GetFileName(file.FileName);
        string fileSavePath = Path.Combine(uploadFolder, fileName);

        using (FileStream stream = new FileStream(fileSavePath, FileMode.Create))
        {
            await file.CopyToAsync(stream);
        }
    }

    public async Task<Category> GetNullCategoryAsync(string categoryName)
    {
        return await _bookRepository.GetBookCategory(categoryName);
    }

    public void UpdateBookInformation(Books book, CreateBookViewModel viewModel, Category category)
    {
        book.Title = viewModel.Title;
        book.Description = viewModel.Description;
        book.Author = viewModel.Author;
        book.Price = viewModel.Price;
        book.StockQuantity = viewModel.StockQuantity;
        book.Category = category;
        book.CategoryId = category.Id;
        book.CreatedAt = viewModel.CreatedAt;
        book.CoverImage = viewModel.CoverImage;
    }
    public async Task UpdateBookAsync(CreateBookViewModel viewModel, IFormFile file, string rootPath)
    {
        if (file != null && file.Length>0)
        {
            SaveImageToFile(file, rootPath);
            viewModel.CoverImage = "/uploads/" + file.FileName;
        }
        
        var category = await _bookRepository.GetBookCategory(viewModel.Category);
        var book = await _bookRepository.GetByIdAsync(viewModel.BookId);

        if (book == null) 
        {
            throw new InvalidOperationException("Book Doesn't Exists!");
        }

        UpdateBookInformation(book, viewModel, category);
  
        await _repository.UpdateAsync<Books>(book);
        await _repository.SaveChangesAsync();
    }

    public async Task DeleteBookAsync(int id)
    {
        var book = await _bookRepository.GetByIdAsync(id);
        if (book != null)
        {
            await _repository.DeleteAsync<Books>(book);
            await _repository.SaveChangesAsync();
        }
    }
}
