using System.ComponentModel.DataAnnotations;

namespace BoiPoka.Models;

public class Books
{
    [Key]
    public int BookId { get; set; }
    public string Title { get; set; }
    public string Description { get; set; }
    public string Author { get; set; }
    public int Price { get; set; }
    public int StockQuantity { get; set; }
    public int CategoryId { get; set; }
    public Category Category { get; set; }
    public string CoverImage { get; set; }
    public DateTime CreatedAt { get; set; }
}
