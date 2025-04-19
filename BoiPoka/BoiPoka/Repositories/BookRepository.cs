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

    public async Task<List<Books>> GetAllAsync() => await _context.Books.ToListAsync();

    public async Task<Books?> GetByIdAsync(int id) => await _context.Books.FindAsync(id);

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

