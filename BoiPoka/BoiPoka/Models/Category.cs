using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;

namespace BoiPoka.Models;

public class Category
{
    public int Id { get; set; }
    public string Name { get; set; }
    [ValidateNever]
    public ICollection<Books> Books { get; set; }
}
