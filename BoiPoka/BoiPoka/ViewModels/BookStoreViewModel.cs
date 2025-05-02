using BoiPoka.Models;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using Microsoft.AspNetCore.Mvc.Rendering;
namespace BoiPoka.ViewModels;

public class BookStoreViewModel
{
    [ValidateNever]
    public IEnumerable<Books> Books { get; set; }
    [ValidateNever]
    public IEnumerable<SelectListItem> CategoryList { get; set; }
    public string? SelectedCategoryName { get; set; }
    public string SearchQuery { get; set; }

}
