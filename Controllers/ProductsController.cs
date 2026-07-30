using HomeMarket.DTOs.Product;
using HomeMarket.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace HomeMarket.Controllers
{
    [Route("api/[controller]/v1")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        // GET: api/<ProductsController>
        private readonly IProductService _productService;
        private readonly IImageService _imageService;
        private readonly ICategoryService _categoryService;
        private readonly ILogger<ProductsController> _logger;

        public ProductsController(IProductService productService, IImageService imageService, ILogger<ProductsController> logger, ICategoryService categoryService)
        {
            _productService = productService;
            _imageService = imageService;
            _logger = logger;
            _categoryService = categoryService;
        }

        [HttpGet]
        public async Task<IActionResult> GetProducts()
        {
            try
            {
                var product = await _productService.GetProductsAsync();
                if (product == null || !product.Any())
                {
                    return NotFound();
                }
                return Ok(product);
            }
            catch (Exception ex) { 

                return StatusCode(500, ex.StackTrace);
            }
            
        }

        [HttpGet("get-product-by-id/{id:int}")]
        public async Task<IActionResult> GetProduct(int id)
        {
            try
            {
                var product = await _productService.GetProductByIdAsync(id);

                if (product == null)
                    return NotFound($"product with id {id} was not found");

                return Ok(product);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.StackTrace);
            }
            
        }

        [HttpPost("create-product")]
        public async Task<IActionResult> CreateProduct(CreateProductDto dto)
        {
            try
            {
                if (dto == null)
                { return BadRequest(); }

                var category = await _categoryService.GetCategoryByIdAsync(dto.CategoryId);

                if(category == null)
                {
                    return BadRequest("Category does not exist");
                }
                var product = await _productService.CreateProductAsync(dto);

                return CreatedAtAction(nameof(GetProduct), new { id = product.ProductId }, product);
            }
            catch (Exception ex) {

                return StatusCode(500, $"Failed to create product {dto.Name}");
            }
           
        }

        [HttpPut]
        public async Task<IActionResult> UpdateProduct(UpdateProductDto dto)
        {
            try
            {
                if (dto == null)
                { return BadRequest(); }

                //Validate category for product
                var category = await _categoryService.GetCategoryByIdAsync(dto.CategoryId);

                if (category == null)
                {
                    return BadRequest("Category does not exist");

                }

                var product = await _productService.UpdateProductAsync(dto);

                if(product == null)
                {
                    return StatusCode(500, "Failed to update product");
                }

                return Ok();
            }
            catch (Exception ex) { 
                return StatusCode(500, ex.StackTrace);
            }
           
        }

        [HttpDelete("{id:int}")]
        public async Task<IActionResult> DeleteProduct(int id)
        {

            try
            {
                //Validate product to be deleted
                var product = await _productService.GetProductByIdAsync(id);

                if (id < 1)
                    return BadRequest();

                if (product == null)
                    return NotFound($"product with id {id} was not found");


                await _productService.DeleteProductAsync(id);
                return Ok();
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.StackTrace);
            }
            
            
        }

        //[HttpPatch("{id:int}/availability")]
        //public async Task<IActionResult> UpdateAvailability(
        //    int id,
        //    [FromBody] bool available)
        //{
        //    await _productService.UpdateAvailabilityAsync(id, available);

        //    return NoContent();
        //}

        [HttpPost("upload-image")]
        public async Task<IActionResult> UploadImage(IFormFile file)
        {
            var url = await _imageService.UploadImageAsync(file);

            return Ok(new
            {
                ImageUrl = url
            });
        }
    }
}
