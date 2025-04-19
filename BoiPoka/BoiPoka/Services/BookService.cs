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

    public async Task<List<Books>> GetAllBooksAsync() => await _repository.GetAllAsync();

    public async Task<Books?> GetBookByIdAsync(int id) => await _repository.GetByIdAsync(id);

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

        var book = new Books
        {
            Title = viewModel.Title,
            Description = viewModel.Description,
            Author = viewModel.Author,
            Price = viewModel.Price,
            StockQuantity = viewModel.StockQuantity,
            Category = viewModel.Category,
            CreatedAt = viewModel.CreatedAt,
            CoverImage = viewModel.CoverImage
        };

        await _repository.AddAsync(book);
    }

    public async Task UpdateBookAsync(Books book) => await _repository.UpdateAsync(book);

    public async Task DeleteBookAsync(int id)
    {
        var book = await _repository.GetByIdAsync(id);
        if (book != null)
        {
            await _repository.DeleteAsync(book);
        }
    }
}

