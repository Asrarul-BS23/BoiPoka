using BoiPoka.Data;
using Microsoft.EntityFrameworkCore;

namespace BoiPoka.Repositories;

public class Repository: IRepository
{
    private readonly AppDbContext _context;
    public Repository(AppDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<T>> GetAllTsAsync<T>() where T : class
    {
        return await _context.Set<T>().ToListAsync();
    }

    public async Task AddAsync<T>(T entity) where T : class
    {
        await _context.Set<T>().AddAsync(entity);
    }

    public async Task<T> FindByIdAsync<T>(int id) where T : class
    {
        return await _context.Set<T>().FindAsync(id);
    }

    public async Task UpdateAsync<T>(T entity) where T : class
    {
        _context.Set<T>().Update(entity);
    }

    public async Task DeleteAsync<T>(T entity) where T : class
    {
        _context.Set<T>().Remove(entity);
    }

    public async Task SaveChangesAsync()
    {
        await _context.SaveChangesAsync();
    }
}
