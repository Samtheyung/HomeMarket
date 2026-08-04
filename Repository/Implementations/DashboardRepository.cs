using HomeMarket.DbProfile;
using HomeMarket.DTOs.Dashboard;
using HomeMarket.Models.DbModels;
using HomeMarket.Repository.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.Identity.Client;

namespace HomeMarket.Repository.Implementations
{
    public class DashboardRepository : IDashboardRepository
    {
        private readonly HomeMarketDbContext _context;

        public DashboardRepository(HomeMarketDbContext context)
        {
            _context = context;
        }

        public async Task<int> GetTotalOrdersAsync()
        {
            return await _context.Orders.CountAsync();
        }

        public async Task<int> GetPendingOrdersAsync()
        {
            return await _context.Orders
                .CountAsync(x => x.Status == OrderStatus.Pending);
        }

        public async Task<int> GetDeliveredOrdersAsync()
        {
            return await _context.Orders
                .CountAsync(x => x.Status == OrderStatus.Delivered);
        }

        public async Task<decimal> GetTotalRevenueAsync()
        {
            return await _context.Orders
                .Where(x => x.Status == OrderStatus.Delivered)
                .SumAsync(x => (decimal?)x.TotalAmount) ?? 0;
        }

        public async Task<int> GetTotalCustomersAsync()
        {
            return await _context.Customers.CountAsync();
        }

        public async Task<int> GetTotalProductsAsync()
        {
            return await _context.Products.CountAsync();
        }

        public async Task<IEnumerable<TopSellingProductDto>> GetTopSellingProductsAsync()
        {
            return await _context.OrderItems
                .Include(x => x.Product)
                .GroupBy(x => new
                {
                    x.ProductId,
                    x.Product.Name
                })
                .Select(g => new TopSellingProductDto
                {
                    ProductId = g.Key.ProductId,
                    ProductName = g.Key.Name,
                    QuantitySold = g.Sum(x => x.Quantity),
                    Revenue = g.Sum(x => x.TotalPrice)
                })
                .OrderByDescending(x => x.QuantitySold)
                .Take(10)
                .ToListAsync();
        }

        public async Task<decimal> GetRevenueAsync(
            DateTime startDate,
            DateTime endDate)
        {
            return await _context.Orders
                .Where(x =>
                    x.Status == OrderStatus.Delivered &&
                    x.OrderDate >= startDate &&
                    x.OrderDate <= endDate)
                .SumAsync(x => (decimal?)x.TotalAmount) ?? 0;
        }

        public async Task<int> GetPreparingOrdersAsync()
        {
            return await _context.Orders.CountAsync(x => x.Status == OrderStatus.Preparing);


            //throw new NotImplementedException();
        }

        public async Task<int> GetCancelledOrdersAsync()
        {
            return await _context.Orders.CountAsync(x => x.Status == OrderStatus.Cancelled);
            //throw new NotImplementedException();
        }

        public async Task<int> GetAvailableProductsAsync()
        {
            return await _context.Products.CountAsync(x => x.IsAvailable);
        }

        public async Task<IEnumerable<Order>> GetRecentOrdersAsync(int count)
        {
                    return await _context.Orders
            .Include(x => x.Customer)
            .OrderByDescending(x => x.OrderDate)
            .Take(count)
            .ToListAsync();
            //throw new NotImplementedException();
        }

        public async Task<IEnumerable<Order>> GetOrdersByDateAsync(DateTime date)
        {
            return await _context.Orders
                .Include(x => x.Customer)
                .Where(x => x.OrderDate.Date == date.Date)
                .ToListAsync();
            //throw new NotImplementedException();
        }

        public async Task<IEnumerable<Order>> GetOrdersBetweenDatesAsync(DateTime startDate, DateTime endDate)
        {
            return await _context.Orders
                .Include(x => x.Customer)
                .Where(x => x.OrderDate >= startDate && x.OrderDate <= endDate)
                .ToListAsync();
            //throw new NotImplementedException();
        }

        public async Task<IEnumerable<TopSellingProductDto>> GetTopSellingProductsAsync(int count)
        {
            return await _context.OrderItems
                .Include(x => x.Product)
                .GroupBy(x => new
                {
                    x.ProductId,
                    x.Product.Name
                })
                .Select(g => new TopSellingProductDto
                {
                    ProductId = g.Key.ProductId,
                    ProductName = g.Key.Name,
                    QuantitySold = g.Sum(x => x.Quantity),
                    Revenue = g.Sum(x => x.TotalPrice)
                })
                .OrderByDescending(x => x.QuantitySold)
                .Take(count)
                .ToListAsync();
            //throw new NotImplementedException();
        }

        Task<IEnumerable<Product>> IDashboardRepository.GetMostViewedProductsAsync(int count)
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<Product>> GetUnavailableProductsAsync()
        {
            return await _context.Products
                .Where(x => !x.IsAvailable)
                .ToListAsync();
            //throw new NotImplementedException();
        }

        public async Task<IEnumerable<TopCustomerDto>> GetTopCustomersAsync(int count)
        {
            return await _context.Orders
                .Include(x => x.Customer)
                .GroupBy(x => new
                {
                    x.CustomerId,
                    x.Customer.FirstName,
                    x.Customer.LastName
                })
                .Select(g => new TopCustomerDto
                {
                    CustomerId = g.Key.CustomerId,
                    FullName = $"{g.Key.FirstName} {g.Key.LastName}",
                    TotalOrders = g.Count(),
                    TotalSpent = g.Sum(x => x.TotalAmount)
                })
                .OrderByDescending(x => x.TotalSpent)
                .Take(count)
                .ToListAsync();
            //throw new NotImplementedException();
        }

        public async Task<Customers?> GetBestCustomerAsync()
        {
            var customer = await _context.Orders
                .Include(x => x.Customer)
                .GroupBy(x => new
                {
                    x.CustomerId,
                    x.Customer.FirstName,
                    x.Customer.LastName
                })
                .Select(g => new
                {
                    CustomerId = g.Key.CustomerId,
                    FullName = $"{g.Key.FirstName} {g.Key.LastName}",
                    TotalSpent = g.Sum(x => x.TotalAmount)
                })
                .OrderByDescending(x => x.TotalSpent).FirstOrDefaultAsync();

            if (customer == null)
            {
                throw new Exception("No customers found.");
            }

            return new Customers
            {
                CustomerId = customer.CustomerId,
                FirstName = customer.FullName.Split(' ')[0],
                LastName = customer.FullName.Split(' ').Length > 1 ? customer.FullName.Split(' ')[1] : string.Empty
            };
            //throw new NotImplementedException();
        }

        public async Task<IEnumerable<DailySalesDto>> GetDailySalesAsync(DateTime startDate, DateTime endDate)
        {
            return await _context.Orders
                .Where(x =>
                    x.Status == OrderStatus.Delivered &&
                    x.OrderDate >= startDate &&
                    x.OrderDate <= endDate)
                .GroupBy(x => x.OrderDate.Date)
                .Select(g => new DailySalesDto
                {
                    Date = g.Key.Date,
                    Revenue = g.Sum(x => x.TotalAmount)
                })
                .OrderBy(x => x.Date)
                .ToListAsync();
            //throw new NotImplementedException();
        }

        public async Task<IEnumerable<MonthlySalesDto>> GetMonthlySalesAsync(int year)
        {
            return await _context.Orders
                .Where(x =>
                    x.Status == OrderStatus.Delivered &&
                    x.OrderDate.Year == year)
                .GroupBy(x => x.OrderDate.Month)
                .Select(g => new MonthlySalesDto
                {
                    Month = g.Key,
                    Revenue = g.Sum(x => x.TotalAmount)
                })
                .OrderBy(x => x.Month)
                .ToListAsync();
            //throw new NotImplementedException();
        }

        public async Task<IEnumerable<CategorySalesDto>>GetSalesByCategoryAsync()
        {
            return await _context.OrderItems
                .Include(x => x.Product)
                .ThenInclude(p => p.Category)
                .Where(x => x.Product.Category != null)
                .GroupBy(x => new
                {
                    x.Product.CategoryId,
                    x.Product.Category.Name
                })
                .Select(g => new CategorySalesDto
                {
                    CategoryId = g.Key.CategoryId,
                    CategoryName = g.Key.Name,
                    Revenue = g.Sum(x => x.TotalPrice)
                })
                .OrderByDescending(x => x.Revenue)
                .ToListAsync();
            //throw new NotImplementedException();
        }
    }
}
