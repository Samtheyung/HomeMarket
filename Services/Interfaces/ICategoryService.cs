using HomeMarket.DTOs.Category;

namespace HomeMarket.Services.Interfaces
{
    public interface ICategoryService
    {
        Task<IEnumerable<CategoryDto>> GetCategoriesAsync();

        Task<CategoryDto?> GetCategoryByIdAsync(int categoryId);

        Task<CategoryDto> CreateCategoryAsync(CreateCategoryDto dto);

        Task<CategoryDto> UpdateCategoryAsync(UpdateCategoryDto dto);

        Task DeleteCategoryAsync(int categoryId);
    }
}
