using BoiPoka.Models;

namespace BoiPoka.ViewModels
{
    public class CheckoutViewModel
    {
        public int OrderId { get; set; }
        public string ReceiverName { get; set; }
        public string ReceiverAddress { get; set; }
        public string ReceiverPhone { get; set; }
        public DateTime OrderDate { get; set; }
        public int DeliveryCharge { get; set; }
        public ICollection<OrderItem> OrderItems { get; set; } = new List<OrderItem>();
        public decimal Subtotal => OrderItems.Sum(item => item.Quantity * item.UnitPrice);
        public decimal TotalAmount => Subtotal + DeliveryCharge;
        public int OrderStatus { get; set; }
        public string PaymentMethod { get; set; }
    }
}
