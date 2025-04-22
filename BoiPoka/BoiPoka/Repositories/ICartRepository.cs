using BoiPoka.Models;

namespace BoiPoka.Repositories;
public interface ICartRepository
{
    Task<Cart> GetCartByUserIdAsync(string userId);
    Task<Books> GetBookByIdAsync(int bookId);
    Task<CartItem> GetCartItemByIdAsync(int cartItemId);
    Task AddCartAsync(Cart cart);
    Task SaveChangesAsync();
    Task RemoveFromCartItemAsync(CartItem cartItem);
}
