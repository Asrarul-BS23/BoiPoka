using BoiPoka.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using BoiPoka.Services;

namespace BoiPoka.Controllers;

[Authorize]
public class OrderController : Controller
{
    private readonly IOrderServices _orderServices;
    private readonly UserManager<Users> _userManager;
    public OrderController(IOrderServices orderServices, UserManager<Users> userManager)
    {
        _orderServices = orderServices;
        _userManager = userManager;
    }
    public IActionResult Index()
    {
        return View();
    }
    public async Task<IActionResult> Checkout()
    {
        var user = await _userManager.GetUserAsync(User);
        var order = _orderServices.GetOrderAsync(user.Id);
        return View(order);
    }
    [HttpPost]
    public async Task<IActionResult> Checkout(Order order)
    {
        if (!ModelState.IsValid)
        {
            return View(order);
        }
        var user = await _userManager.GetUserAsync(User);
        var cart = await _orderServices.GetCartAsync(user.Id);
        if (cart == null || !cart.CartItems.Any())
        {
            ModelState.AddModelError("", "Cart is empty.");
            return View(order);
        }
        try
        {
            _orderServices.PlaceOrderAsync(order, cart, user.Id);

            return RedirectToAction("OrderHistory", "Order");
        }
        catch (DbUpdateException ex)
        {
            ModelState.AddModelError("", "An error occurred while saving the order. Please try again.");
        }
        catch (Exception ex)
        {
            ModelState.AddModelError("", "Unexpected error: " + ex.Message);
        }
        return View(order);
    }
    public async Task<IActionResult> OrderHistory()
    {
        var user = await _userManager.GetUserAsync(User);
        var orders = await _orderServices.GetOrderHistoryAsync(user.Id);
        return View(orders);
    }
    [Authorize(Roles ="Admin")]
    public async Task<IActionResult> ManageOrders()
    {
        var orders = await _orderServices.GetAllOrderAsync();
        return View(orders);
    }
    [Authorize(Roles = "Admin")]
    [HttpPost]
    public async Task<IActionResult> ManageOrders(int orderId, int orderStatus)
    {
        var order = await _orderServices.FindOrderByIdAsync(orderId);
        if (order != null)
        {
            await _orderServices.UpdateOrderStatusAsync(order, orderStatus);
            return RedirectToAction("ManageOrders", "Order");
        }
        return View(order);
    }
}
