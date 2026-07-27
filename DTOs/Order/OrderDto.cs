using HomeMarket.DTOs.Customer;
using HomeMarket.Models.DbModels;

namespace HomeMarket.DTOs.Order
{
    public class OrderDto
    {
        public int OrderId { get; set; }

        public CustomerDto Customer { get; set; }

        public DateTime OrderDate { get; set; }

        public decimal TotalAmount { get; set; }

        public PaymentMethod PaymentMethod { get; set; }

        public OrderStatus Status { get; set; }

        public List<OrderItemDto> Items { get; set; }
    }
}
