using HomeMarket.Models.DbModels;

namespace HomeMarket.Repository.Implementations
{
    public interface ICustomersRepository
    {
        Task<Customers?> GetByIdAsync(int customerId);

        Task<IEnumerable<Customers>> GetAllAsync();

        Task<Customers?> FindAsync(string email, string phoneNumber);

        Task AddAsync(Customers customer);

        Task UpdateAsync(Customers customer);

        Task SaveChangesAsync();
    }
}
