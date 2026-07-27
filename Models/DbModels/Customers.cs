using System.ComponentModel.DataAnnotations;

namespace HomeMarket.Models.DbModels
{
    public class Customers
    {
        [Key]
        public int CustomerId { get; set; }

        [Required]
        [MaxLength(100)]
        public string FirstName { get; set; }

        [Required]
        [MaxLength(100)]
        public string LastName { get; set; }

        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        [Phone]
        public string PhoneNumber { get; set; }

        [Required]
        public string DeliveryAddress { get; set; }

        public ICollection<Order> Orders { get; set; } = new List<Order>();
    }
}
