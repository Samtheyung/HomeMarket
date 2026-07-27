using System.ComponentModel.DataAnnotations;

namespace HomeMarket.DTOs.Product
{
    public class UpdateProductDto
    {
        public int ProductId { get; set; }

        [Required]
        public string Name { get; set; }

        public string? Description { get; set; }

        public decimal Price { get; set; }

        public string? ImageUrl { get; set; }

        public bool IsAvailable { get; set; }

        public int CategoryId { get; set; }
    }
}
