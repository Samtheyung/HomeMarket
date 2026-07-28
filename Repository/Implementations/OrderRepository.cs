using HomeMarket.DbProfile;
using HomeMarket.Models.DbModels;
using Microsoft.EntityFrameworkCore;

namespace HomeMarket.Repository.Implementations
{
    public class OrderRepository : IOrderRepository
    {
        private readonly HomeMarketDbContext _context;

        public OrderRepository(HomeMarketDbContext context)
        {
            _context = context;
        }

        public async Task<Order?> GetByIdAsync(int orderId)
        {
            return await _context.Orders
                .Include(x => x.Customer)
                .Include(x => x.Items)
                    .ThenInclude(i => i.Product)
                .FirstOrDefaultAsync(x => x.OrderId == orderId);
        }

        public async Task<IEnumerable<Order>> GetAllAsync()
        {
            return await _context.Orders
                .Include(x => x.Customer)
                .Include(x => x.Items)
                    .ThenInclude(i => i.Product)
                .OrderByDescending(x => x.OrderDate)
                .ToListAsync();
        }

        public async Task<IEnumerable<Order>> GetByStatusAsync(OrderStatus status)
        {
            return await _context.Orders
                .Include(x => x.Customer)
                .Include(x => x.Items)
                    .ThenInclude(i => i.Product)
                .Where(x => x.Status == status)
                .OrderByDescending(x => x.OrderDate)
                .ToListAsync();
        }

        public async Task AddAsync(Order order)
        {
            await _context.Orders.AddAsync(order);
            await SaveChangesAsync();
        }

        public async Task UpdateAsync(Order order)
        {
            _context.Orders.Update(order);
            await SaveChangesAsync();
        }

        public async Task DeleteAsync(Order order)
        {
            _context.Orders.Remove(order);
            await SaveChangesAsync();
        }

        public async Task SaveChangesAsync()
        {
            await _context.SaveChangesAsync();
        }
    }
}
