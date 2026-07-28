using HomeMarket.Models.DbModels;

namespace HomeMarket.Repository.Implementations
{
    public interface IOrderRepository
    {
        Task<Order?> GetByIdAsync(int orderId);

        Task<IEnumerable<Order>> GetAllAsync();

        Task<IEnumerable<Order>> GetByStatusAsync(OrderStatus status);

        Task AddAsync(Order order);

        Task UpdateAsync(Order order);

        Task DeleteAsync(Order order);

        Task SaveChangesAsync();
    }
}
