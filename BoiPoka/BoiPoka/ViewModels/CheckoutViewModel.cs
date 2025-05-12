using BoiPoka.Models;
using System.ComponentModel.DataAnnotations;

namespace BoiPoka.ViewModels
{
    public class CheckoutViewModel
    {
        public int OrderId { get; set; }
        public string ReceiverName { get; set; }
        public string ReceiverAddress { get; set; }
        [Required(ErrorMessage = "Phone Number is Required!")]
        [RegularExpression(@"^01\d{9}", ErrorMessage ="Phone Number must be 11 digits and start with '01'.")]
        public string ReceiverPhone { get; set; }
        public DateTime OrderDate { get; set; }
        public int DeliveryCharge { get; set; }
        public ICollection<OrderItem> OrderItems { get; set; } = new List<OrderItem>();
        public decimal Subtotal => OrderItems.Sum(item => item.Quantity * item.UnitPrice);
        public decimal TotalAmount => Subtotal + DeliveryCharge;
        public string OrderStatus { get; set; }
        public string PaymentMethod { get; set; }
    }
}
