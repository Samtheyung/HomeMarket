using HomeMarket.Models.DbModels;

namespace HomeMarket.Services.Interfaces
{
    public interface INotificationService
    {
        Task SendOrderConfirmationAsync(Order orderId);


        Task SendNewOrderNotificationAsync(Order orderId);


        Task SendOrderStatusUpdateAsync(Order orderId);
    }
}
