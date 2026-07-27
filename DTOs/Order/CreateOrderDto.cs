using HomeMarket.DTOs.Customer;
using HomeMarket.Models.DbModels;
using System.ComponentModel.DataAnnotations;

namespace HomeMarket.DTOs.Order
{
    public class CreateOrderDto
    {
        [Required]
        public CreateCustomerDto Customer { get; set; }

        [Required]
        public PaymentMethod PaymentMethod { get; set; }

        [Required]
        public List<CreateOrderItemDto> Items { get; set; }
    }
}
