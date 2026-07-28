using HomeMarket.DTOs.Customer;
using HomeMarket.Models.DbModels;

namespace HomeMarket.Services.Interfaces
{
    public interface ICustomerService
    {
        Task<CustomerDto> CreateCustomerAsync(CreateCustomerDto dto);

        Task<Customers?> FindCustomerAsync(string email, string phone);
    }
}
