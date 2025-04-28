using BoiPoka.Data;
using BoiPoka.Models;
using BoiPoka.ViewModels;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace BoiPoka.Repositories;

public class HomeRepository : IHomeRepository
{
    private readonly AppDbContext _context;
    public HomeRepository(AppDbContext context)
    {
        _context = context;
    }
    public async Task<IEnumerable<SelectListItem>> GetCategoryListAsync()
    {
        var categories = await _context.Categories.ToListAsync();
        var categoryList = categories.Select(
            c => new SelectListItem
            {
                Text = c.Name,
                Value = c.Name
            });
        return categoryList;
    }
    public async Task<IEnumerable<Books>> GetAllBooksAsync()
    {
        return await _context.Books.ToListAsync();
    }
    public async Task<IEnumerable<Books>> FilterAndSearchAsync(BookStoreViewModel bookStoreViewModel)
    {
        var books = await _context.Books
                .Include(b=>b.Category)
                .Where(b=> 
                    b.Category.Name == bookStoreViewModel.SelectedCategoryName && 
                    b.Title.ToLower().Contains(bookStoreViewModel.SearchQuery.ToLower())
                )
                .ToListAsync();
        return books;
    }
    public async Task<IEnumerable<Books>> FilterByCategoryAsync(BookStoreViewModel bookStoreViewModel)
    {
        return await _context.Books
            .Where(b => b.Category.Name == bookStoreViewModel.SelectedCategoryName)
            .ToListAsync();
    }
    public async Task<IEnumerable<Books>> FilterBySearchAsync(BookStoreViewModel bookStoreViewModel)
    {
        return await _context.Books.Where(b => b.Title.ToLower().Contains(bookStoreViewModel.SearchQuery)).ToListAsync();
    }    
}
