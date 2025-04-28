using Microsoft.EntityFrameworkCore;
using BoiPoka.Models;
namespace BoiPoka.Data;

public class BookSeeder
{
    private readonly BooksAndAuthors data = new BooksAndAuthors();
    public async Task SeedBookAsync(AppDbContext context)
    {
        if (context.Books.Count() > 100) return;
        var books = new List<Books>();
        List<string> bookTitles = data.GetBookTitles();
        List<string> authors = data.GetBookAuthors();
        Random random = new Random();
        for (int i = 0; i < Math.Min(bookTitles.Count,authors.Count); i++)
        {
            books.Add(
                new Books { 
                    Title = bookTitles[i],
                    Author = authors[i],
                    Price = random.Next(100,400),
                    CreatedAt = DateTime.Now,
                    CategoryId = random.Next(1,5),
                    StockQuantity = random.Next(1,50),
                    Description = $"Description of {bookTitles[i]}",
                    CoverImage = "/uploads/default.jpg",
                }
                );
        }
        await context.Books.AddRangeAsync(books);
        await context.SaveChangesAsync();
    }
}
