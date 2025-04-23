namespace BoiPoka.Models;

public class OrderItem
{
    public int OrderItemId { get; set; }
    public Order Order { get; set; }
    public Books Book { get; set; }
    public int OrderId { get; set; }
    public int BookId { get; set; }
    public int Quantity { get; set; }
    public decimal UnitPrice { get; set; }
    public decimal TotalPrice { get; set; }

}
