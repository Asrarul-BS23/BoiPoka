using BoiPoka.Models;
namespace BoiPoka.Services;

public interface IOrderServices
{
    Task<Order> GetOrderAsync(string userId);
    Task PlaceOrderAsync(Order order, Cart cart, string userId);
    Task<ICollection<Order>> GetOrderHistoryAsync(string userId);
    Task<ICollection<Order>> GetAllOrderAsync();
    Task<Cart> GetCartAsync(string userId);
    Task<Order> FindOrderByIdAsync(int orderId);
    Task UpdateOrderStatusAsync(Order order, int orderStatus);

}
