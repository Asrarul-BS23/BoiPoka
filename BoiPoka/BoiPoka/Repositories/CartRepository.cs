﻿using BoiPoka.Data;
using BoiPoka.Models;
using Microsoft.EntityFrameworkCore;

namespace BoiPoka.Repositories;

public class CartRepository : ICartRepository
{
    private readonly AppDbContext _context;

    public CartRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task<Cart> GetCartByUserIdAsync(string userId)
    {
        return await _context.Carts
            .Include(c => c.CartItems)
            .ThenInclude(ci => ci.Book)
            .FirstOrDefaultAsync(c => c.UserId == userId);
    }

    public async Task<CartItem> GetCartItemByIdAsync(int cartItemId)
    {
        return await _context.CartItems
            .Include(ci => ci.Book)
            .FirstOrDefaultAsync(ci => ci.Id == cartItemId);
    }

    public async Task<int> GetCartItemCountAsync(string userId)
    {
        return await _context.Carts
            .Where(c => c.UserId == userId)
            .SelectMany(c => c.CartItems)
            .SumAsync(ci => ci.Quantity);
    }
}

