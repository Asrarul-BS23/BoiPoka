using BoiPoka.Extensions;
using BoiPoka.Models;
using BoiPoka.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace BoiPoka.Controllers;

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
    public async Task<IActionResult> AddToCart(int bookId, string returnUrl)
    {
        var user = await _userManager.GetUserAsync(User);
        await _cartService.AddToCartAsync(user.Id, bookId);
        return Redirect(returnUrl ?? "/");
    }
    [HttpPost]
    public async Task<IActionResult> AddToCartSession(int bookId, string returnUrl)
    {
        var cart = HttpContext.Session.GetObject<List<int>>("Cart") ?? new List<int>();
        cart.Add(bookId);
        HttpContext.Session.SetObject("Cart", cart);
        return Redirect(returnUrl ?? "/");  
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

