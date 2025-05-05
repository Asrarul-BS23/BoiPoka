using BoiPoka.Data;
using BoiPoka.Models;
using BoiPoka.ViewModels;
using Microsoft.EntityFrameworkCore;

namespace BoiPoka.Repositories;

public class BookRepository : IBookRepository
{
    private readonly AppDbContext _context;

    public BookRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<T>> GetAllTsAsync<T>() where T : class
    {
        return await _context.Set<T>().ToListAsync();
    }

    public async Task<Books?> GetByIdAsync(int id) => await _context.Books
        .Include(b => b.Category)
        .FirstOrDefaultAsync(b=> b.BookId == id);

    public async Task<Category> GetBookCategory(string categoryName) => await _context.Categories.FirstOrDefaultAsync(cg => cg.Name == categoryName);
    public async Task CreateCategory(Category category)
    {
        await _context.Categories.AddAsync(category);
        await _context.SaveChangesAsync();
    }
    public async Task AddAsync(Books book)
    {
        _context.Books.Add(book);
        await _context.SaveChangesAsync();
    }

    public async Task UpdateAsync(Books book)
    {
        _context.Books.Update(book);
        await _context.SaveChangesAsync();
    }

    public async Task DeleteAsync(Books book)
    {
        _context.Books.Remove(book);
        await _context.SaveChangesAsync();
    }
}

