using AutoMapper;
using HomeMarket.DTOs.Product;
using HomeMarket.Models.DbModels;
using HomeMarket.Repository.Implementations;
using HomeMarket.Services.Interfaces;

namespace HomeMarket.Services.Implementations
{
    public class ProductService : IProductService
    {
        private readonly IProductRepository _productRepository;
        private readonly IMapper _mapper;

        public ProductService(
            IProductRepository productRepository,
            IMapper mapper)
        {
            _productRepository = productRepository;
            _mapper = mapper;
        }


        public async Task<IEnumerable<ProductDto>> GetProductsAsync()
        {
            var products = await _productRepository.GetAllAsync();

            return _mapper.Map<IEnumerable<ProductDto>>(products);
        }


        public async Task<ProductDto?> GetProductByIdAsync(int productId)
        {
            var product = await _productRepository.GetByIdAsync(productId);

            if (product == null)
                return null;

            return _mapper.Map<ProductDto>(product);
        }


        public async Task<ProductDto> CreateProductAsync(CreateProductDto dto)
        {
            var product = _mapper.Map<Product>(dto);

            await _productRepository.AddAsync(product);

            return _mapper.Map<ProductDto>(product);
        }


        public async Task<ProductDto> UpdateProductAsync(UpdateProductDto dto)
        {
            var product = await _productRepository.GetByIdAsync(dto.ProductId);

            if (product == null)
                throw new Exception("Product not found");


            product.Name = dto.Name;
            product.Description = dto.Description;
            product.Price = dto.Price;
            product.ImageUrl = dto.ImageUrl;
            product.IsAvailable = dto.IsAvailable;
            product.CategoryId = dto.CategoryId;


            await _productRepository.UpdateAsync(product);

            return _mapper.Map<ProductDto>(product);
        }


        public async Task DeleteProductAsync(int productId)
        {
            var product = await _productRepository.GetByIdAsync(productId);

            if (product == null)
                throw new Exception("Product not found");


            await _productRepository.DeleteAsync(product);
        }
    }
}
