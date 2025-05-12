using BoiPoka.Models;
using BoiPoka.Repositories;
using BoiPoka.ViewModels;

namespace BoiPoka.Services;

public class OrderServices : IOrderServices
{
    private readonly IOrderRepository _orderRepository;
    private readonly IRepository _repository;

    public OrderServices(IOrderRepository orderRepository, IRepository repository)
    {
        _orderRepository = orderRepository;
        _repository = repository;
    }

    public async Task<IEnumerable<Order>> GetAllOrderAsync()
    {
        return await _orderRepository.GetAllOrdersAsync();
    }

    public async Task<Order> GetOrderAsync(string userId)
    {
        var cart = await _orderRepository.GetCartByUserIdAsync(userId);
        if (cart == null)
        {
            throw new InvalidOperationException("Cart Not Found!");
        }
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
    public async void UpdateBookQuantity(IEnumerable<OrderItem> orderItems)
    {
        foreach (var item in orderItems)
        {
            Books book = await _repository.FindByIdAsync<Books>(item.BookId);
            book.StockQuantity = Math.Max(0, book.StockQuantity - item.Quantity);
            await _repository.UpdateAsync<Books>(book);
            await _repository.SaveChangesAsync();
        }
    }
    public List<OrderItem> GetOrderItems(Cart cart)
    {
        var orderItems = cart.CartItems.Select(ci => new OrderItem
        {
            BookId = ci.BookId,
            Quantity = ci.Quantity,
            UnitPrice = ci.Book.Price,
            TotalPrice = ci.Quantity * ci.Book.Price,
        }).ToList();
        return orderItems;
    }
    public CheckoutViewModel GetCheckoutViewModel(Order order)
    {
        var checkoutOrder = new CheckoutViewModel
        {
            OrderId = order.OrderId,
            ReceiverName = order.ReceiverName,
            ReceiverAddress = order.ReceiverAddress,
            ReceiverPhone = order.ReceiverPhone,
            OrderDate = order.OrderDate,
            DeliveryCharge = order.DeliveryCharge,
            OrderItems = order.OrderItems,
            OrderStatus = order.OrderStatus,
            PaymentMethod = order.PaymentMethod
        };
        return checkoutOrder;
    }
    public async Task<Order> GetPopulatedOrder(CheckoutViewModel checkoutOrder, Cart cart, string userId)
    {
        var orderItems = GetOrderItems(cart);
            
        UpdateBookQuantity(orderItems);

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
        await _repository.AddAsync<Order>(order);
        await _orderRepository.RemoveRangeCartItemsAsync(cart);
        await _repository.DeleteAsync<Cart>(cart);
        await _repository.SaveChangesAsync();

    }

    public async Task<Order> FindOrderByIdAsync(int orderId)
    {
        return await _repository.FindByIdAsync<Order>(orderId);
    }
    public async Task UpdateOrderStatusAsync(Order order, string orderStatus)
    { 
        order.OrderStatus = orderStatus;

        await _repository.UpdateAsync<Order>(order);
        await _repository.SaveChangesAsync();
    }
}
