using BoiPoka.Models;
namespace BoiPoka.Repositories;

public interface IOrderRepository
{
    Task<Cart> GetCartByUserIdAsync(string userId);
    Task AddOrderAsync(Order order);
    Task RemoveRangeCartItemsAsync(Cart cart);
    Task RemoveCartAsync(Cart cart);
    Task SaveChangesAsync();

    Task<ICollection<Order>> GetOrderByUserIdAsync(string userId);
    Task<ICollection<Order>> GetAllOrdersAsync();
    Task<Order> FindOrderByIdAsync(int orderId);
}
