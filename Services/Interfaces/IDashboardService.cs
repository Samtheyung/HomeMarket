using HomeMarket.DTOs.Customer;
using HomeMarket.DTOs.Dashboard;
using HomeMarket.DTOs.Order;
using HomeMarket.DTOs.Product;

namespace HomeMarket.Services.Interfaces
{
    public interface IDashboardService
    {
        Task<DashboardDto> GetDashboardAsync();

        Task<decimal> GetRevenueAsync(DateTime startDate, DateTime endDate);

        Task<IEnumerable<OrderDto>> GetRecentOrdersAsync(int count);

        Task<IEnumerable<OrderDto>> GetOrdersByDateAsync(DateTime date);

        Task<IEnumerable<OrderDto>> GetOrdersBetweenDatesAsync(DateTime startDate, DateTime endDate);

        Task<IEnumerable<TopSellingProductDto>> GetTopSellingProductsAsync(int count);

        Task<IEnumerable<ProductDto>> GetUnavailableProductsAsync();
        Task<IEnumerable<ProductDto>> GetAvailableProductsAsync();

        Task<IEnumerable<TopCustomerDto>> GetTopCustomersAsync(int count);

        Task<CustomerDto?> GetBestCustomerAsync();

        Task<IEnumerable<DailySalesDto>> GetDailySalesAsync(DateTime startDate, DateTime endDate);

        Task<IEnumerable<MonthlySalesDto>> GetMonthlySalesAsync(int year);

        Task<IEnumerable<CategorySalesDto>> GetSalesByCategoryAsync();
    }
}
