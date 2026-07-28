using AutoMapper;
using HomeMarket.DTOs.Category;
using HomeMarket.Models.DbModels;
using HomeMarket.Repository.Implementations;
using HomeMarket.Services.Interfaces;

namespace HomeMarket.Services.Implementations
{
    public class CategoryService : ICategoryService
    {
        private readonly ICategoryRepository _repository;
        private readonly IMapper _mapper;


        public CategoryService(
            ICategoryRepository repository,
            IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }


        public async Task<IEnumerable<CategoryDto>> GetCategoriesAsync()
        {
            var categories =
                await _repository.GetAllAsync();


            return _mapper.Map<IEnumerable<CategoryDto>>(categories);
        }


        public async Task<CategoryDto> CreateCategoryAsync( CreateCategoryDto dto)
        {
            var category = _mapper.Map<Category>(dto);

            await _repository.AddAsync(category);

            return _mapper.Map<CategoryDto>(category);
        }

        public async Task<CategoryDto?> GetCategoryByIdAsync(int categoryId)
        {
            var category = await _repository.GetByIdAsync(categoryId);

            if (category == null)
                return null;

            return _mapper.Map<CategoryDto>(category);
        }

        public async Task<CategoryDto> UpdateCategoryAsync(UpdateCategoryDto dto)
        {
            var category = await _repository.GetByIdAsync(dto.CategoryId);

            if (category == null)
                throw new Exception("Category not found.");

            // Check for duplicate name
            var categories = await _repository.GetAllAsync();

            if (categories.Any(c =>
                c.CategoryId != dto.CategoryId &&
                c.Name.Trim().ToLower() == dto.Name.Trim().ToLower()))
            {
                throw new Exception("Another category already has this name.");
            }

            category.Name = dto.Name;
            category.Description = dto.Description;

            await _repository.UpdateAsync(category);

            return _mapper.Map<CategoryDto>(category);
        }

        public async Task DeleteCategoryAsync(int categoryId)
        {
            var category = await _repository.GetByIdAsync(categoryId);

            if (category == null)
                throw new Exception("Category not found.");

            // Prevent deleting categories that still have products
            if (category.Products != null && category.Products.Any())
            {
                throw new Exception(
                    "Cannot delete a category that contains products.");
            }

            await _repository.DeleteAsync(category);
        }

    }
}
