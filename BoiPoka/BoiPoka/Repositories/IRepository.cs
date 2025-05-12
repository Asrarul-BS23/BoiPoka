using BoiPoka.Models;

namespace BoiPoka.Repositories;

public interface IRepository
{
    Task<IEnumerable<T>> GetAllTsAsync<T>() where T : class;
    Task<T?> FindByIdAsync<T>(int id) where T: class;
    Task AddAsync<T>(T t) where T: class;
    Task UpdateAsync<T>(T t) where T : class;
    Task DeleteAsync<T>(T t) where T : class;
    Task SaveChangesAsync();
}
