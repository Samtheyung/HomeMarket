using HomeMarket.Models.DbModels;

namespace HomeMarket.Repository.Implementations
{
    public interface ICategoryRepository
    {
        Task<IEnumerable<Category>> GetAllAsync();

        Task<Category?> GetByIdAsync(int categoryId);

        Task AddAsync(Category category);

        Task UpdateAsync(Category category);

        Task DeleteAsync(Category category);

        Task SaveChangesAsync();
    }
}
