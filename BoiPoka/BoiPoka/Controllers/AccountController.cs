using Microsoft.AspNetCore.Mvc;

using BoiPoka.ViewModels;
using BoiPoka.Services;

namespace BoiPoka.Controllers;

public class AccountController : Controller
{
    private readonly IAccountService _accountService;

    public AccountController(IAccountService accountService)
    {
        _accountService = accountService;
    }

    [HttpGet]
    public IActionResult Login()
    { 
        return View(); 
    }

    [HttpPost]
    public async Task<IActionResult> Login(LoginViewModel model)
    {
        if (!ModelState.IsValid)
        {
            return View(model);
        }

        var (success, error) = await _accountService.LoginAsync(model, HttpContext);
        if (success)
        {
            return RedirectToAction("Index", "Home");
        }

        ModelState.AddModelError("", error);
        return View(model);
    }

    [HttpGet]
    public IActionResult Register()
    {
        return View();
    }

    [HttpPost]
    public async Task<IActionResult> Register(RegisterViewModel model)
    {
        if (!ModelState.IsValid)
        {
            return View(model);
        }

        var (success, errors) = await _accountService.RegisterAsync(model);
        if (success)
        {
            return RedirectToAction("Login");
        }

        foreach (var err in errors)
        {
            ModelState.AddModelError("", err);
        }
        return View(model);
    }

    [HttpGet]
    public IActionResult VerifyEmail()
    {
        return View();
    }

    [HttpPost]
    public async Task<IActionResult> VerifyEmail(VerifyEmailViewModel model)
    {
        if (!ModelState.IsValid)
        {
            return View();
        }

        var username = await _accountService.VerifyEmailAsync(model);
        if (username == null)
        {
            ModelState.AddModelError("", "Something is wrong!");
            return View(model);
        }

        return RedirectToAction("ChangePassword", new { username });
    }

    [HttpGet]
    public IActionResult ChangePassword(string username)
    {
        if (string.IsNullOrEmpty(username))
        {
            return RedirectToAction("VerifyEmail");
        }
        return View(new ChangePasswordViewModel { Email = username });
    }

    [HttpPost]
    public async Task<IActionResult> ChangePassword(ChangePasswordViewModel model)
    {
        if (!ModelState.IsValid)
        {
            ModelState.AddModelError("", "Something went wrong. Try again!");
            return View(model);
        }

        var (success, error) = await _accountService.ChangePasswordAsync(model);
        if (success)
        {
            return RedirectToAction("Login");
        }

        ModelState.AddModelError("", error);
        return View(model);
    }

    public async Task<IActionResult> Logout()
    {
        await _accountService.LogoutAsync();
        return RedirectToAction("Index", "Home");
    }
}