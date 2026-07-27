using System.ComponentModel.DataAnnotations;

namespace HomeMarket.DTOs.Product
{
    public class CreateProductDto
    {
        [Required]
        public string Name { get; set; }

        public string? Description { get; set; }

        [Range(0.01, double.MaxValue)]
        public decimal Price { get; set; }

        public string? ImageUrl { get; set; }

        public int CategoryId { get; set; }
    }
}
