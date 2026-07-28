using HomeMarket.DbProfile;
using HomeMarket.DTOs.Dashboard;
using HomeMarket.Models.DbModels;
using HomeMarket.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace HomeMarket.Services.Implementations
{
    public class DashboardService : IDashboardService
    {
        private readonly HomeMarketDbContext _context;


        public DashboardService(HomeMarketDbContext context)
        {
            _context = context;
        }



        public async Task<DashboardDto>
            GetDashboardAsync()
        {

            var dashboard = new DashboardDto
            {
                TotalOrders =
                    await _context.Orders.CountAsync(),


                PendingOrders =
                    await _context.Orders
                    .CountAsync(x =>
                        x.Status ==
                        OrderStatus.Pending),


                DeliveredOrders =
                    await _context.Orders
                    .CountAsync(x =>
                        x.Status ==
                        OrderStatus.Delivered),


                TotalRevenue =
                    await _context.Orders
                    .Where(x =>
                        x.Status ==
                        OrderStatus.Delivered)
                    .SumAsync(x =>
                        x.TotalAmount)
            };


            return dashboard;
        }





        public async Task<IEnumerable<TopSellingProductDto>>
            GetTopSellingProductsAsync()
        {

            return await _context.OrderItems

                .GroupBy(x => new
                    {
                        x.ProductId,
                        x.Product.Name
                    })

                .Select(x =>
                    new TopSellingProductDto
                    {
                        ProductId =
                            x.Key.ProductId,

                        ProductName =
                            x.Key.Name,

                        QuantitySold =
                            x.Sum(i =>
                                i.Quantity)
                    })

                .OrderByDescending(x =>
                    x.QuantitySold)

                .Take(10)

                .ToListAsync();
        }




        public async Task<decimal> GetRevenueAsync(DateTime startDate, DateTime endDate)
        {

            return await _context.Orders

                .Where(x => x.OrderDate >= startDate && x.OrderDate <= endDate && x.Status == OrderStatus.Delivered).SumAsync(x => x.TotalAmount);
        }
    }
}
