using BoiPoka.Data;
using BoiPoka.Models;
using BoiPoka.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BoiPoka.Controllers;

[Authorize]
public class CartController : Controller
{
    private readonly ICartService _cartService;
    private readonly UserManager<Users> _userManager;

    public CartController(ICartService cartService, UserManager<Users> userManager)
    {
        _cartService = cartService;
        _userManager = userManager;
    }

    public async Task<IActionResult> Index()
    {
        var user = await _userManager.GetUserAsync(User);
        var cart = await _cartService.GetCartAsync(user.Id);
        return View(cart);
    }

    [HttpPost]
    public async Task<IActionResult> AddToCart(int bookId)
    {
        var user = await _userManager.GetUserAsync(User);
        await _cartService.AddToCartAsync(user.Id, bookId);
        return RedirectToAction(nameof(Index));
    }

    [HttpPost]
    public async Task<IActionResult> UpdateCart(int cartItemId, int quantity)
    {
        await _cartService.UpdateCartItemAsync(cartItemId, quantity);
        return RedirectToAction(nameof(Index));
    }

    [HttpPost]
    public async Task<IActionResult> RemoveFromCart(int cartItemId)
    {
        await _cartService.RemoveFromCartAsync(cartItemId);
        return RedirectToAction(nameof(Index));
    }
}

