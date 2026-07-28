using HomeMarket.DTOs.Dashboard;

namespace HomeMarket.Services.Interfaces
{
    public interface IDashboardService
    {
        Task<DashboardDto> GetDashboardAsync();


        Task<IEnumerable<TopSellingProductDto>> GetTopSellingProductsAsync();


        Task<decimal> GetRevenueAsync(DateTime startDate, DateTime endDate);
    }
}
