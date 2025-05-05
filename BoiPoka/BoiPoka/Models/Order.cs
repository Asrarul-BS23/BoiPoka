using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System.ComponentModel.DataAnnotations.Schema;

namespace BoiPoka.Models;

public class Order
{
    public int OrderId { get; set; }
    public string UserId { get; set; }
    [ForeignKey("UserId")]
    [ValidateNever]
    public Users User { get; set; }
    public string ReceiverName { get; set; }
    public string ReceiverAddress { get; set; }
    public string ReceiverPhone { get; set; }
    public DateTime OrderDate { get; set; }
    public int DeliveryCharge { get; set; }
    public ICollection<OrderItem> OrderItems { get; set; } = new List<OrderItem>();
    public decimal Subtotal { get; set; }
    public decimal TotalAmount => Subtotal + DeliveryCharge;
    public string OrderStatus { get; set; }
    public string PaymentMethod { get; set; }

}
