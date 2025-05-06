using BoiPoka.Models;
namespace BoiPoka.Services;

public interface ICartService
{
    Task<Cart> GetCartAsync(string userId);
    Task AddToCartAsync(string userId, int bookId);
    Task UpdateCartItemAsync(int cartItemId, int quantity);
    Task RemoveFromCartAsync(int cartItemId);
    Task<int> GetCartItemCountAsync(string userId);
}


