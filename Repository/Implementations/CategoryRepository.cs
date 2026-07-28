using HomeMarket.DbProfile;
using HomeMarket.Models.DbModels;
using Microsoft.EntityFrameworkCore;

namespace HomeMarket.Repository.Implementations
{
    public class CategoryRepository : ICategoryRepository
    {
        private readonly HomeMarketDbContext _context;

        public CategoryRepository(HomeMarketDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Category>> GetAllAsync()
        {
            return await _context.Categories
                .Include(x => x.Products)
                .ToListAsync();
        }

        public async Task<Category?> GetByIdAsync(int categoryId)
        {
            return await _context.Categories
                .Include(x => x.Products)
                .FirstOrDefaultAsync(x => x.CategoryId == categoryId);
        }

        public async Task AddAsync(Category category)
        {
            await _context.Categories.AddAsync(category);
            await SaveChangesAsync();
        }

        public async Task UpdateAsync(Category category)
        {
            _context.Categories.Update(category);
            await SaveChangesAsync();
        }

        public async Task DeleteAsync(Category category)
        {
            _context.Categories.Remove(category);
            await SaveChangesAsync();
        }

        public async Task SaveChangesAsync()
        {
            await _context.SaveChangesAsync();
        }
    }
}
