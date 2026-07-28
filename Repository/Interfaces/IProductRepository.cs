using HomeMarket.Models.DbModels;

namespace HomeMarket.Repository.Implementations
{
    public interface IProductRepository
    {
        Task<IEnumerable<Product>> GetAllAsync();

        Task<Product?> GetByIdAsync(int productId);

        Task<IEnumerable<Product>> GetByCategoryAsync(int categoryId);

        Task AddAsync(Product product);

        Task UpdateAsync(Product product);

        Task DeleteAsync(Product product);

        Task SaveChangesAsync();
    }
}
