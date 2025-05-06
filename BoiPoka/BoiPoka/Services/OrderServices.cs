using BoiPoka.Models;
using BoiPoka.Repositories;
using BoiPoka.ViewModels;

namespace BoiPoka.Services;

public class OrderServices : IOrderServices
{
    private readonly IOrderRepository _orderRepository;

    public OrderServices(IOrderRepository orderRepository)
    {
        _orderRepository = orderRepository;
    }

    public async Task<ICollection<Order>> GetAllOrderAsync()
    {
        return await _orderRepository.GetAllOrdersAsync();
    }

    public async Task<Order> GetOrderAsync(string userId)
    {
        var cart = await _orderRepository.GetCartByUserIdAsync(userId);
        return new Order { Subtotal = cart.Total, DeliveryCharge = 60, };
    }

    public async Task<ICollection<Order>> GetOrderHistoryAsync(string userId)
    {
        return await _orderRepository.GetOrderByUserIdAsync(userId);
    }
    public async Task<Cart> GetCartAsync(string userId)
    {
        return await _orderRepository.GetCartByUserIdAsync(userId);
    }
    public async Task<Order> GetPopulatedOrder(CheckoutViewModel checkoutOrder, Cart cart, string userId)
    {
        var orderItems = cart.CartItems.Select(ci => new OrderItem
        {
            BookId = ci.BookId,
            Quantity = ci.Quantity,
            UnitPrice = ci.Book.Price,
            TotalPrice = ci.Quantity * ci.Book.Price,
        }).ToList();
        foreach (var item in orderItems)
        {
            Books book = await _orderRepository.FindByIdAsync<Books>(item.BookId);
            book.StockQuantity = book.StockQuantity - item.Quantity;
            await _orderRepository.UpdateStockAsync(book);
        }
        var subtotal = orderItems.Sum(oi => oi.TotalPrice);
        var order = new Order {
            ReceiverName = checkoutOrder.ReceiverName,
            ReceiverAddress = checkoutOrder.ReceiverAddress,
            ReceiverPhone = checkoutOrder.ReceiverPhone,
            PaymentMethod = checkoutOrder.PaymentMethod,
            OrderDate = checkoutOrder.OrderDate,
            OrderStatus = checkoutOrder.OrderStatus,
            UserId = userId,
            OrderItems = orderItems,
            Subtotal = subtotal,
            DeliveryCharge = checkoutOrder.DeliveryCharge,
        };

        return order;
    }
    public async Task PlaceOrderAsync(Order order, Cart cart)
    {
        await _orderRepository.AddOrderAsync(order);
        await _orderRepository.RemoveRangeCartItemsAsync(cart);
        await _orderRepository.RemoveCartAsync(cart);
        await _orderRepository.SaveChangesAsync();
    }

    public async Task<Order> FindOrderByIdAsync(int orderId)
    {
        return await _orderRepository.FindByIdAsync<Order>(orderId);
    }
    public async Task UpdateOrderStatusAsync(Order order, string orderStatus)
    { 
            order.OrderStatus = orderStatus;
            await _orderRepository.SaveChangesAsync();
    }
}
