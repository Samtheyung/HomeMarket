using AutoMapper;
using HomeMarket.DbProfile;
using HomeMarket.DTOs.Customer;
using HomeMarket.DTOs.Dashboard;
using HomeMarket.DTOs.Order;
using HomeMarket.DTOs.Product;
using HomeMarket.Models.DbModels;
using HomeMarket.Repository.Interfaces;
using HomeMarket.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace HomeMarket.Services.Implementations
{
    public class DashboardService : IDashboardService
    {
        private readonly IDashboardRepository _dashboardRepository;
        private readonly IMapper _mapper;

        public DashboardService(IDashboardRepository dashboardRepository, IMapper mapper)
        {
            _dashboardRepository = dashboardRepository;
            _mapper = mapper;
        }

        public async Task<DashboardDto> GetDashboardAsync()
        {
            return new DashboardDto
            {
                TotalOrders = await _dashboardRepository.GetTotalOrdersAsync(),
                PendingOrders = await _dashboardRepository.GetPendingOrdersAsync(),
                DeliveredOrders = await _dashboardRepository.GetDeliveredOrdersAsync(),
                TotalRevenue = await _dashboardRepository.GetTotalRevenueAsync(),
                TotalCustomers = await _dashboardRepository.GetTotalCustomersAsync(),
                TotalProducts = await _dashboardRepository.GetTotalProductsAsync()
            };
        }

        public async Task<IEnumerable<TopSellingProductDto>> GetTopSellingProductsAsync(int count)
        {
            return await _dashboardRepository.GetTopSellingProductsAsync(count);
        }

        public async Task<decimal> GetRevenueAsync(DateTime startDate, DateTime endDate)
        {
            return await _dashboardRepository.GetRevenueAsync(startDate, endDate);
        }

        public async Task<IEnumerable<OrderDto>> GetRecentOrdersAsync(int count)
        {
            var Orders =  await _dashboardRepository.GetRecentOrdersAsync(count);

            return _mapper.Map<IEnumerable<OrderDto>>(Orders);
            //throw new NotImplementedException();
        }

        public async Task<IEnumerable<OrderDto>> GetOrdersByDateAsync(DateTime date)
        {
            var Orders = await _dashboardRepository.GetOrdersByDateAsync(date);
            return _mapper.Map<IEnumerable<OrderDto>>(Orders);
            //throw new NotImplementedException();
        }

        public async Task<IEnumerable<OrderDto>> GetOrdersBetweenDatesAsync(DateTime startDate, DateTime endDate)
        {
            var Orders = await _dashboardRepository.GetOrdersBetweenDatesAsync(startDate, endDate);
            return _mapper.Map<IEnumerable<OrderDto>>(Orders);
            //throw new NotImplementedException();
        }

        public async Task<IEnumerable<ProductDto>> GetUnavailableProductsAsync()
        {
            var products = await _dashboardRepository.GetUnavailableProductsAsync();
            return _mapper.Map<IEnumerable<ProductDto>>(products);
            //throw new NotImplementedException();
        }

        public async Task<IEnumerable<ProductDto>> GetAvailableProductsAsync()
        {
            var products = await _dashboardRepository.GetAvailableProductsAsync();
            return _mapper.Map<IEnumerable<ProductDto>>(products);
            //throw new NotImplementedException();
        }

        public async Task<IEnumerable<TopCustomerDto>> GetTopCustomersAsync(int count)
        {
            var customers = await _dashboardRepository.GetTopCustomersAsync(count);
            return _mapper.Map<IEnumerable<TopCustomerDto>>(customers);
            //throw new NotImplementedException();
        }

        public async Task<CustomerDto?> GetBestCustomerAsync()
        {
            var customer = await _dashboardRepository.GetBestCustomerAsync();
            if (customer == null)
            {
                throw new Exception("No best customer found.");
            }
            return _mapper.Map<CustomerDto>(customer);

            //throw new NotImplementedException();
        }

        public async Task<IEnumerable<DailySalesDto>> GetDailySalesAsync(DateTime startDate, DateTime endDate)
        {
            var dailySales = await _dashboardRepository.GetDailySalesAsync(startDate, endDate);
            return dailySales;
            //throw new NotImplementedException();
        }

        public async Task<IEnumerable<MonthlySalesDto>> GetMonthlySalesAsync(int year)
        {
            return await _dashboardRepository.GetMonthlySalesAsync(year);
            //throw new NotImplementedException();
        }

        public async Task<IEnumerable<CategorySalesDto>> GetSalesByCategoryAsync()
        {
            return await _dashboardRepository.GetSalesByCategoryAsync();
            //throw new NotImplementedException();
        }
    }
    
}
