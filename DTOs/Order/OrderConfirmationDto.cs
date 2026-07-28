using HomeMarket.Models.DbModels;

namespace HomeMarket.DTOs.Order
{
    public class OrderConfirmationDto
    {
        public int OrderId { get; set; }

        public DateTime OrderDate { get; set; }

        public decimal TotalAmount { get; set; }

        public OrderStatus Status { get; set; }

        public string Message { get; set; }
    }
}
