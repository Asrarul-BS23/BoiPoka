using BoiPoka.Models;
using BoiPoka.Repositories;

namespace BoiPoka.Services;

public class CartService : ICartService
{
    private readonly ICartRepository _cartRepository;

    public CartService(ICartRepository cartRepository)
    {
        _cartRepository = cartRepository;
    }

    public async Task<Cart> GetCartAsync(string userId)
    {
        return await _cartRepository.GetCartByUserIdAsync(userId);
    }

    public async Task AddToCartAsync(string userId, int bookId)
    {
        var book = await _cartRepository.GetBookByIdAsync(bookId);
        if (book == null || book.StockQuantity < 1)
            throw new InvalidOperationException("Book not available.");

        var cart = await _cartRepository.GetCartByUserIdAsync(userId);
        if (cart == null)
        {
            cart = new Cart { UserId = userId };
            await _cartRepository.AddCartAsync(cart);
        }

        var existingItem = cart.CartItems.FirstOrDefault(ci => ci.BookId == bookId);
        if (existingItem != null)
        {
            existingItem.Quantity = Math.Min(existingItem.Quantity + 1, book.StockQuantity);
        }
        else
        {
            cart.CartItems.Add(new CartItem
            {
                BookId = bookId,
                Quantity = 1
            });
        }

        await _cartRepository.SaveChangesAsync();
    }

    public async Task UpdateCartItemAsync(int cartItemId, int quantity)
    {
        var cartItem = await _cartRepository.GetCartItemByIdAsync(cartItemId);
        if (cartItem == null)
            throw new InvalidOperationException("Cart item not found.");

        var availableStock = cartItem.Book.StockQuantity;
        cartItem.Quantity = Math.Min(quantity, availableStock);

        if (cartItem.Quantity < 1)
        {
            _cartRepository.RemoveFromCartItemAsync(cartItem);
        }

        await _cartRepository.SaveChangesAsync();
    }

    public async Task RemoveFromCartAsync(int cartItemId)
    {
        var cartItem = await _cartRepository.GetCartItemByIdAsync(cartItemId);
        if (cartItem == null)
            throw new InvalidOperationException("Cart item not found.");
        _cartRepository.RemoveFromCartItemAsync(cartItem);
        await _cartRepository.SaveChangesAsync();
    }

    public async Task<int> GetCartItemCountAsync(string userId)
    {
        return await _cartRepository.GetCartItemCountAsync(userId);
    }
}

