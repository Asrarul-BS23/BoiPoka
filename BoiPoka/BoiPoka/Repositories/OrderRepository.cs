using BoiPoka.Data;
using BoiPoka.Migrations;
using BoiPoka.Models;
using Microsoft.EntityFrameworkCore;

namespace BoiPoka.Repositories;

public class OrderRepository : IOrderRepository
{
    private readonly AppDbContext _context;

    public OrderRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task AddOrderAsync(Order order)
    {
        await _context.Orders.AddAsync(order);
    }

    public async Task<Cart> GetCartByUserIdAsync(string userId)
    {
        return await _context.Carts
            .Include(c => c.CartItems)
            .ThenInclude(ci => ci.Book)
            .FirstOrDefaultAsync(c => c.UserId == userId);
    }

    public async Task RemoveCartAsync(Cart cart)
    {
        await _context.Carts.Remove(cart);
    }

    public async Task RemoveRangeCartItemsAsync(Cart cart)
    {
        await _context.CartItems.RemoveRange(cart.CartItems);
    }

    public async Task SaveChangesAsync()
    {
        await _context.SaveChangesAsync();
    }
    public async Task<ICollection<Order>> GetOrderByUserIdAsync(string userId)
    {
        return await _context.Orders
            .Where(o => o.UserId == userId)
            .Include(o => o.OrderItems)
            .ThenInclude(oi => oi.Book)
            .ToListAsync();
    }
    public async Task<ICollection<Order>> GetAllOrdersAsync()
    {
        return await _context.Orders
            .Include(o => o.User)
            .Include(o => o.OrderItems)
            .ThenInclude(oi => oi.Book)
            .ToListAsync();
    }
    public async Task<Order> FindOrderByIdAsync(int orderId)
    {
        return await _context.Orders.FindAsync(orderId);
    }
}
