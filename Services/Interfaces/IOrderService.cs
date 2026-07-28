using HomeMarket.DTOs.Order;
using HomeMarket.Models.DbModels;

namespace HomeMarket.Services.Interfaces
{
    public interface IOrderService
    {
        Task<OrderConfirmationDto> PlaceOrderAsync(CreateOrderDto dto);

        Task<OrderDto?> GetOrderAsync(int orderId);

        Task UpdateStatusAsync(int orderId, OrderStatus status);
    }
}
