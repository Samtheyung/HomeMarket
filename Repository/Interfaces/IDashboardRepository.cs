using HomeMarket.DTOs.Dashboard;
using HomeMarket.Models.DbModels;

namespace HomeMarket.Repository.Interfaces
{
    public interface IDashboardRepository
    {
        // Dashboard Summary
        Task<int> GetTotalOrdersAsync();
        Task<int> GetPendingOrdersAsync();
        Task<int> GetPreparingOrdersAsync();
        Task<int> GetDeliveredOrdersAsync();
        Task<int> GetCancelledOrdersAsync();

        Task<int> GetTotalCustomersAsync();
        Task<int> GetTotalProductsAsync();
        Task<int> GetAvailableProductsAsync();

        Task<decimal> GetTotalRevenueAsync();
        Task<decimal> GetRevenueAsync(DateTime startDate, DateTime endDate);

        // Orders
        Task<IEnumerable<Order>> GetRecentOrdersAsync(int count);
        Task<IEnumerable<Order>> GetOrdersByDateAsync(DateTime date);
        Task<IEnumerable<Order>> GetOrdersBetweenDatesAsync(DateTime startDate, DateTime endDate);

        // Products
        Task<IEnumerable<TopSellingProductDto>> GetTopSellingProductsAsync(int count);
        Task<IEnumerable<Product>> GetMostViewedProductsAsync(int count); // Optional
        Task<IEnumerable<Product>> GetUnavailableProductsAsync();

        // Customers
        Task<IEnumerable<TopCustomerDto>> GetTopCustomersAsync(int count);
        Task<Customers?> GetBestCustomerAsync();

        // Reports
        Task<IEnumerable<DailySalesDto>> GetDailySalesAsync(DateTime startDate, DateTime endDate);
        Task<IEnumerable<MonthlySalesDto>> GetMonthlySalesAsync(int year);
        Task<IEnumerable<CategorySalesDto>> GetSalesByCategoryAsync();
    }
}
