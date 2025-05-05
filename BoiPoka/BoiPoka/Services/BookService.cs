using BoiPoka.ViewModels;
using BoiPoka.Models;
using BoiPoka.Repositories;

namespace BoiPoka.Services;

public class BookService : IBookService
{
    private readonly IBookRepository _repository;

    public BookService(IBookRepository repository)
    {
        _repository = repository;
    }

    public async Task<IEnumerable<T>> GetAllTsAsync<T>() where T : class
    {
        return await _repository.GetAllTsAsync<T>();
    }
    public async Task<Books?> GetBookByIdAsync(int id) => await _repository.GetByIdAsync(id);

    public async Task CreateNewCategoryAsync(Category category) => await _repository.CreateCategory(category);
    public async Task CreateBookAsync(CreateBookViewModel viewModel, IFormFile file, string rootPath)
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

        viewModel.CoverImage = "/uploads/" + fileName;

        var category = await _repository.GetBookCategory(viewModel.Category);

        var book = new Books
        {
            Title = viewModel.Title,
            Description = viewModel.Description,
            Author = viewModel.Author,
            Price = viewModel.Price,
            StockQuantity = viewModel.StockQuantity,
            Category = category,
            CategoryId = category.Id,
            CreatedAt = viewModel.CreatedAt,
            CoverImage = viewModel.CoverImage
        };

        await _repository.AddAsync(book);
    }


    public async Task<Category> GetNullCategoryAsync(string categoryName)
    {
        return await _repository.GetBookCategory(categoryName);
    }
    public async Task UpdateBookAsync(CreateBookViewModel viewModel, IFormFile file, string rootPath)
    {
        if (file != null && file.Length>0)
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
            viewModel.CoverImage = "/uploads/" + fileName;
        }
        
        var category = await _repository.GetBookCategory(viewModel.Category);
        var book = await _repository.GetByIdAsync(viewModel.BookId);
        book.Title = viewModel.Title;
        book.Description = viewModel.Description;
        book.Author = viewModel.Author;
        book.Price = viewModel.Price;
        book.StockQuantity = viewModel.StockQuantity;
        book.Category = category;
        book.CategoryId = category.Id;
        book.CreatedAt = viewModel.CreatedAt;
        book.CoverImage = viewModel.CoverImage;
  
        await _repository.UpdateAsync(book);
    }

    public async Task DeleteBookAsync(int id)
    {
        var book = await _repository.GetByIdAsync(id);
        if (book != null)
        {
            await _repository.DeleteAsync(book);
        }
    }
}
