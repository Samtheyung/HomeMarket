using HomeMarket.DTOs.Customer;
using HomeMarket.DTOs.Order;
using HomeMarket.Models.DbModels;

namespace HomeMarket.Services.Interfaces
{
    public interface ICustomerService
    {
        Task<CustomerDto> CreateCustomerAsync(CreateCustomerDto dto);
        Task<CustomerDto> GetCustomerByIdAsync(int customerId);

        Task<IEnumerable<OrderDto>> GetCustomerOrdersAsync(int customerId);

        Task<IEnumerable<CustomerDto>> GetCustomerAsync();

        Task<Customers?> FindCustomerAsync(string email, string phone);
    }
}
