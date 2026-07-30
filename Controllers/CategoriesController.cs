using HomeMarket.DTOs.Category;
using HomeMarket.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace HomeMarket.Controllers
{
    [Route("api/[controller]/v1")]
    [ApiController]
    public class CategoriesController : ControllerBase
    {
        // GET: api/<CategoriesController>
        private readonly ICategoryService _categoryService;
        private readonly ILogger<CategoriesController> _logger;

        public CategoriesController(ICategoryService categoryService, ILogger<CategoriesController> logger)
        {
            _categoryService = categoryService;
            _logger = logger;
        }

        [HttpGet]
        public async Task<IActionResult> GetCategories()
        {
            try
            {
                var categories = await _categoryService.GetCategoriesAsync();
                if (categories == null || !categories.Any())
                {
                    return NotFound("No categories found.");
                }
                return Ok(categories);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.StackTrace);
            }
            
        }

        [HttpGet("get-category-by-id/{id:int}")]
        public async Task<IActionResult> GetCategory(int id)
        {
            try
            {
                var category = await _categoryService.GetCategoryByIdAsync(id);

                if (category == null)
                    return NotFound($"category with id {id} not found");

                return Ok(category);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.StackTrace);
            }
            
        }

        [HttpPost("create-category")]
        public async Task<IActionResult> CreateCategory([FromBody] CreateCategoryDto dto)
        {
            try
            {
                if (dto == null) {
                    return BadRequest("Please enter correct category details");
                }
                var category = await _categoryService.CreateCategoryAsync(dto);

                if (category == null)
                {
                    return StatusCode(200,"Failed to create a category");
                }
                return CreatedAtAction(nameof(GetCategory), new { id = category.CategoryId }, category);
            }
            catch (Exception ex) 
            {
                return StatusCode(500, ex.StackTrace);
            }
            
        }

        [HttpPut("update-category-by-id")]
        public async Task<IActionResult> UpdateCategory([FromBody] UpdateCategoryDto dto)
        {
            try
            {
                if (dto == null)
                {
                    return BadRequest();
                }
                var category = await _categoryService.UpdateCategoryAsync(dto);

                return Ok(category);
            }
            catch(Exception ex)
            {
                return StatusCode(500, ex.StackTrace);
            }
            
        }

        [HttpDelete("delete-category-by-id/{id:int}")]
        public async Task<IActionResult> DeleteCategory(int id)
        {
            try
            {
                await _categoryService.DeleteCategoryAsync(id);

                return Ok();
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.StackTrace);
            }

        }
    }
}

