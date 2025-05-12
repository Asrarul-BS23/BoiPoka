using BoiPoka.Models;
using BoiPoka.ViewModels;
using BoiPoka.Extensions;
using Microsoft.AspNetCore.Identity;

namespace BoiPoka.Services;

public class AccountService : IAccountService
{
    private readonly UserManager<Users> _userManager;
    private readonly SignInManager<Users> _signInManager;
    private readonly ICartService _cartService;

    public AccountService(
        UserManager<Users> userManager,
        SignInManager<Users> signInManager,
        ICartService cartService)
    {
        _userManager = userManager;
        _signInManager = signInManager;
        _cartService = cartService;
    }

    public async Task<(bool success, IEnumerable<string> errors)> RegisterAsync(RegisterViewModel model)
    {
        var user = new Users
        {
            FullName = model.Name,
            Email = model.Email,
            UserName = model.Email
        };

        var result = await _userManager.CreateAsync(user, model.Password);

        if (!result.Succeeded)
        {
            return (false, result.Errors.Select(e => e.Description));
        }

        var roleResult = await _userManager.AddToRoleAsync(user, "Member");

        if (!roleResult.Succeeded)
        {
            return (false, roleResult.Errors.Select(e => e.Description));
        }

        return (true, Enumerable.Empty<string>());
    }

    public async Task<(bool success, string errorMessage)> LoginAsync(LoginViewModel model, HttpContext httpContext)
    {
        var result = await _signInManager.PasswordSignInAsync(model.Email, model.Password, model.RememberMe, false);
        if (result.Succeeded)
        {
            await MergeCartIntoUserCart(httpContext);
            return (true, null);
        }
        return (false, "Email or Password is incorrect!");
    }

    public async Task LogoutAsync()
    {
        await _signInManager.SignOutAsync();
    }

    public async Task<(bool success, string errorMessage)> ChangePasswordAsync(ChangePasswordViewModel model)
    {
        var user = await _userManager.FindByNameAsync(model.Email);
        if (user == null)
        {
            return (false, "Email not found!");
        }

        var result = await _userManager.RemovePasswordAsync(user);
        if (!result.Succeeded)
        {
            return (false, string.Join(", ", result.Errors.Select(e => e.Description)));
        }

        result = await _userManager.AddPasswordAsync(user, model.NewPassword);
        if (!result.Succeeded)
        {
            return (false, string.Join(", ", result.Errors.Select(e => e.Description)));
        }

        return (true, null);
    }

    public async Task<string> VerifyEmailAsync(VerifyEmailViewModel model)
    {
        var user = await _userManager.FindByNameAsync(model.Email);
        return user?.UserName;
    }

    private async Task MergeCartIntoUserCart(HttpContext httpContext)
    {
        var sessionCart = httpContext.Session.GetObject<List<int>>("Cart");
        if (sessionCart != null && sessionCart.Any())
        {
            var user = await _userManager.GetUserAsync(httpContext.User);
            if (!await _userManager.IsInRoleAsync(user, "Admin"))
            {
                foreach (var bookId in sessionCart)
                {
                    await _cartService.AddToCartAsync(user.Id, bookId);
                }
            }
            httpContext.Session.Remove("Cart");
        }
    }
}
