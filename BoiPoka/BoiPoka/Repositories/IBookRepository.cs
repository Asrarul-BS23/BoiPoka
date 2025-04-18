using BoiPoka.Models;

namespace BoiPoka.Repositories;

public interface IBookRepository
{
    Task<List<Books>> GetAllAsync();
    Task<Books?> GetByIdAsync(int id);
    Task AddAsync(Books book);
    Task UpdateAsync(Books book);
    Task DeleteAsync(Books book);
}
