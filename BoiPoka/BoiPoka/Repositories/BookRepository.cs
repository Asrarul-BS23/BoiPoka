using BoiPoka.Data;
using BoiPoka.Models;
using Microsoft.EntityFrameworkCore;

namespace BoiPoka.Repositories;

public class BookRepository : IBookRepository
{
    private readonly AppDbContext _context;

    public BookRepository(AppDbContext context)
    {
        _context = context;
    }

    
    public async Task<Books?> GetByIdAsync(int id)
    {
        return await _context.Books
        .Include(b => b.Category)
        .FirstOrDefaultAsync(b => b.BookId == id);
    }

    public async Task<Category> GetBookCategory(string categoryName)
    {
        return await _context.Categories
            .FirstOrDefaultAsync(cg => cg.Name == categoryName);
    }
}

