using BoiPoka.Models;
namespace BoiPoka.Repositories;

public interface IOrderRepository
{
    Task<Cart> GetCartByUserIdAsync(string userId);
    Task RemoveRangeCartItemsAsync(Cart cart);
    Task<ICollection<Order>> GetOrderByUserIdAsync(string userId);
    Task<ICollection<Order>> GetAllOrdersAsync();
}
