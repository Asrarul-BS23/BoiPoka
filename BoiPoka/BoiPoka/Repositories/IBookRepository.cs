﻿using BoiPoka.Models;
using BoiPoka.ViewModels;

namespace BoiPoka.Repositories;

public interface IBookRepository
{
    Task<IEnumerable<T>> GetAllTsAsync<T>() where T : class;
    Task<Books?> GetByIdAsync(int id);
    Task AddAsync(Books book);
    Task UpdateAsync(Books book);
    Task DeleteAsync(Books book);
    Task CreateCategory(Category category);
    Task<Category> GetBookCategory(string categoryName);
}
