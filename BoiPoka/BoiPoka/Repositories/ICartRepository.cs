using BoiPoka.Models;

namespace BoiPoka.Repositories;
public interface ICartRepository
{
    Task<Cart> GetCartByUserIdAsync(string userId);
    Task<CartItem> GetCartItemByIdAsync(int cartItemId);
    Task<int> GetCartItemCountAsync(string userId);
}
