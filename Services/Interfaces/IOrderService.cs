using HomeMarket.DTOs.Category;
using HomeMarket.DTOs.Order;
using HomeMarket.Models.DbModels;

namespace HomeMarket.Services.Interfaces
{
    public interface IOrderService
    {
        Task<IEnumerable<OrderDto>> GetAllOrdersAsync();
        Task<OrderConfirmationDto> PlaceOrderAsync(CreateOrderDto dto);

        Task<OrderDto?> GetOrderAsync(int orderId);
        Task<IEnumerable<OrderDto?>> GetOrdersByStatusAsync(OrderStatus status);

        Task UpdateStatusAsync(int orderId, OrderStatus status);

        Task CancelOrderAsync(int order);
    }
}
