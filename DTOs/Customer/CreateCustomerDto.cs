using System.ComponentModel.DataAnnotations;

namespace HomeMarket.DTOs.Customer
{
    public class CreateCustomerDto
    {
        [Required]
        public string FirstName { get; set; }

        [Required]
        public string LastName { get; set; }

        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        public string PhoneNumber { get; set; }

        [Required]
        public string DeliveryAddress { get; set; }
    }
}
