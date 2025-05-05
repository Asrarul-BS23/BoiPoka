using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace BoiPoka.Models;

public class Cart
{
    public int Id { get; set; }
    public string UserId { get; set; }
    
    [ForeignKey("UserId")]
    public Users User { get; set; }
    public ICollection<CartItem> CartItems { get; set; } = new List<CartItem>();

    [NotMapped]
    public decimal Subtotal => CartItems.Sum(item => item.Quantity * item.Book.Price);
    
    [NotMapped]
    public decimal Total => Subtotal;
}
