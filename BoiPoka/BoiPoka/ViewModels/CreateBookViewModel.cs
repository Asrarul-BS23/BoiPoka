using BoiPoka.Models;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;

namespace BoiPoka.ViewModels;

public class CreateBookViewModel
{
    [Key]
    public int BookId { get; set; }
    [Required(ErrorMessage = "Title is required!")]
    public string Title { get; set; }
    public string Description { get; set; }
    [Required(ErrorMessage = "Author is required!")]
    public string Author { get; set; }
    [Required(ErrorMessage = "Price is required!")]
    public int Price { get; set; }
    [Required(ErrorMessage = "Stock Quantity is required!")]
    public int StockQuantity { get; set; }
    [Required(ErrorMessage = "Category is required!")]
    public string Category { get; set; }
    [ValidateNever]
    public List<SelectListItem> CategoryList { get; set; }
    public string CoverImage { get; set; }
    [DataType(DataType.DateTime)]
    public DateTime CreatedAt { get; set; } = DateTime.Now;
}
