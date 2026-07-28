using HomeMarket.DTOs.Product;

namespace HomeMarket.Services.Interfaces
{
    public interface IProductService
    {
        Task<IEnumerable<ProductDto>> GetProductsAsync();

        Task<ProductDto?> GetProductByIdAsync(int productId);

        Task<ProductDto> CreateProductAsync(CreateProductDto dto);

        Task<ProductDto> UpdateProductAsync(UpdateProductDto dto);

        Task DeleteProductAsync(int productId);
    }
}
