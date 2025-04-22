namespace BoiPoka.Models;

public class CartItem
{
    public int Id { get; set; }
    public int CartId { get; set; }
    public int BookId { get; set; }
    public int Quantity { get; set; }
    public Cart Cart { get; set; }
    public Books Book { get; set; }
}
