using HomeMarket.DbProfile;
using HomeMarket.Models.DbModels;
using Microsoft.EntityFrameworkCore;

namespace HomeMarket.Repository.Implementations
{
    public class CustomersRepository : ICustomersRepository
    {
        private readonly HomeMarketDbContext _context;

        public CustomersRepository(HomeMarketDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Customers>> GetAllAsync()
        {
            return await _context.Customers
                .Include(x => x.Orders)
                .ToListAsync();
        }

        public async Task<Customers?> GetByIdAsync(int customerId)
        {
            return await _context.Customers
                .Include(x => x.Orders)
                .FirstOrDefaultAsync(x => x.CustomerId == customerId);
        }

        public async Task<Customers?> FindAsync(string email, string phoneNumber)
        {
            return await _context.Customers
                .FirstOrDefaultAsync(x =>
                    x.Email == email ||
                    x.PhoneNumber == phoneNumber);
        }

        public async Task AddAsync(Customers customer)
        {
            await _context.Customers.AddAsync(customer);
            await SaveChangesAsync();
        }

        public async Task UpdateAsync(Customers customer)
        {
            _context.Customers.Update(customer);
            await SaveChangesAsync();
        }

        public async Task SaveChangesAsync()
        {
            await _context.SaveChangesAsync();
        }
    }
}
