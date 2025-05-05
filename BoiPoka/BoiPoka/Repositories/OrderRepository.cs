using BoiPoka.Data;
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

    public Task RemoveCartAsync(Cart cart)
    {
        _context.Carts.Remove(cart);
        return Task.CompletedTask;
    }

    public Task RemoveRangeCartItemsAsync(Cart cart)
    {
        _context.CartItems.RemoveRange(cart.CartItems);
        return Task.CompletedTask;
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
            .OrderByDescending(o => o.OrderDate)
            .ToListAsync();
    }

    public async Task UpdateStockAsync(Books book)
    {
        _context.Update(book);
        await _context.SaveChangesAsync();
    }
   
    public async Task<ICollection<Order>> GetAllOrdersAsync()
    {
        return await _context.Orders
            .Include(o => o.User)
            .Include(o => o.OrderItems)
            .ThenInclude(oi => oi.Book)
            .OrderByDescending (o => o.OrderDate)
            .ToListAsync();
    }

    public async Task<T> FindByIdAsync<T>(int id) where T : class
    {
        return await _context.Set<T>().FindAsync(id);
    }
}
