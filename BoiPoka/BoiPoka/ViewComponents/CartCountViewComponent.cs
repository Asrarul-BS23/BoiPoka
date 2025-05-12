using BoiPoka.Extensions;
using BoiPoka.Models;
using BoiPoka.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace BoiPoka.ViewComponents;

[Authorize]
public class CartCountViewComponent : ViewComponent
{
    private readonly ICartService _cartService;
    private readonly UserManager<Users> _userManager;

    public CartCountViewComponent(ICartService cartService, UserManager<Users> userManager)
    {
        _cartService = cartService;
        _userManager = userManager;
    }

    public async Task<IViewComponentResult> InvokeAsync()
    {
        if (User.Identity?.IsAuthenticated ?? false)
        {
            var user = await _userManager.GetUserAsync((ClaimsPrincipal)User);
            int count = await _cartService.GetCartItemCountAsync(user.Id);
            return View(count);
        }
        else
        {
            var sessionCart = HttpContext.Session.GetObject<List<int>>("Cart");
            return View(sessionCart?.Count ?? 0);
        }
    }
}
