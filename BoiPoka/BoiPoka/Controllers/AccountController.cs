﻿using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using BoiPoka.Models;

using BoiPoka.ViewModels;
using static System.Runtime.InteropServices.JavaScript.JSType;
using BoiPoka.Extensions;
using BoiPoka.Services;

namespace BoiPoka.Controllers;

public class AccountController : Controller
{
    private readonly SignInManager<Users> _signInManager;
    private readonly UserManager<Users> _userManager;
    private readonly ICartService _cartService;

    public AccountController(
        SignInManager<Users> signInManager, 
        UserManager<Users> userManager,
        ICartService cartService)
    {
        _signInManager = signInManager;
        _userManager = userManager;
        _cartService = cartService;
    }

    public IActionResult Login()
    {
        return View();
    }
    [HttpPost]
    public async Task<IActionResult> Login(LoginViewModel model)
    {
        if (ModelState.IsValid)
        {
            var result = await _signInManager.PasswordSignInAsync(model.Email, model.Password, model.RememberMe, false);
            if (result.Succeeded)
            {
                var sessionCart = HttpContext.Session.GetObject<List<int>>("Cart");
                if (sessionCart != null && sessionCart.Any())
                {
                    var user = await _userManager.GetUserAsync(User);
                    if (!User.IsInRole("Admin"))
                    {
                        foreach (var bookId in sessionCart)
                        {
                            await _cartService.AddToCartAsync(user.Id, bookId);
                        }
                    }
                    HttpContext.Session.Remove("Cart");
                }

                return RedirectToAction("Index", "Home");
            }
            else 
            {
                ModelState.AddModelError("", "Email or Password is incorrect!");
                return View(model);
            }
        }
        return View(model);
    }
    public IActionResult Register()
    {
        return View();
    }
    [HttpPost]
    public async Task<IActionResult> Register(RegisterViewModel model)
    {
        if (!ModelState.IsValid)
            return View(model);
        Users users = new Users
        {
            FullName = model.Name,
            Email = model.Email,
            UserName = model.Email,
        };
        var result = await _userManager.CreateAsync(users, model.Password);

        if(!result.Succeeded)
        {
            foreach (var error in result.Errors)
            {
                ModelState.AddModelError("", error.Description);
            }
            return View(model);
        }
        var roleResult = await _userManager.AddToRoleAsync(users, "Member");
        if(!roleResult.Succeeded)
        {
            foreach (var error in roleResult.Errors)
                ModelState.AddModelError("", error.Description);
            return View(model);
        }
        return RedirectToAction("Login", "Account");

    }
    public IActionResult VerifyEmail()
    {
        return View();
    }
    [HttpPost]
    public async Task<IActionResult> VerifyEmail(VerifyEmailViewModel model)
    {
        if (ModelState.IsValid)
        {
            var user = await _userManager.FindByNameAsync(model.Email);
            if (user == null)
            {
                ModelState.AddModelError("", "Something is wrong!");
                return View(model);
            }
            else
            {
                return RedirectToAction("ChangePassword", "Account", new { username = user.UserName });
            }
        }
        return View();
    }

    public IActionResult ChangePassword(string username)
    {
        if (string.IsNullOrEmpty(username))
        {
            return RedirectToAction("VerifyEmail", "Action");

        }
        return View(new ChangePasswordViewModel {Email = username});
    }
    [HttpPost]
    public async Task<IActionResult> ChangePassword(ChangePasswordViewModel model)
    {
        if (ModelState.IsValid)
        {
            var user = await _userManager.FindByNameAsync(model.Email);
            if (user != null)
            {
                var result = await _userManager.RemovePasswordAsync(user);
                if (result.Succeeded)
                {
                    result = await _userManager.AddPasswordAsync(user, model.NewPassword);
                    return RedirectToAction("Login", "Account");
                }
                else
                {
                    foreach (var error in result.Errors)
                    {
                        ModelState.AddModelError("", error.Description);
                    }
                    return View(model);
                }
            }
            else
            {
                ModelState.AddModelError("", "Email not found!");
                return View(model);
            }
        }
        else
        {
            ModelState.AddModelError("", "Something went wrong. Try again!");
            return View(model);
        }
    }
    public async Task<IActionResult> Logout()
    {
        await _signInManager.SignOutAsync();
        return RedirectToAction("Index","Home");
    }
}
