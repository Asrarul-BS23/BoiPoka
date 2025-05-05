using BoiPoka.Models;
namespace BoiPoka.Repositories;

public interface IOrderRepository
{
    Task<Cart> GetCartByUserIdAsync(string userId);
    Task UpdateStockAsync(Books book);
    Task AddOrderAsync(Order order);
    Task RemoveRangeCartItemsAsync(Cart cart);
    Task RemoveCartAsync(Cart cart);
    Task SaveChangesAsync();

    Task<ICollection<Order>> GetOrderByUserIdAsync(string userId);
    Task<ICollection<Order>> GetAllOrdersAsync();
    Task<T> FindByIdAsync<T>(int id) where T : class;
}
