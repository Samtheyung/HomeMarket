using System.ComponentModel.DataAnnotations;

namespace HomeMarket.Models.DbModels
{
    public class Order
    {

        [Key]
        public int OrderId { get; set; }

        public int CustomerId { get; set; }

        public Customers Customer { get; set; }

        public DateTime OrderDate { get; set; } = DateTime.UtcNow;

        public decimal TotalAmount { get; set; }

        public PaymentMethod PaymentMethod { get; set; }

        public OrderStatus Status { get; set; } = OrderStatus.Pending;

        public string? Notes { get; set; }

        public ICollection<OrderItem> Items { get; set; } = new List<OrderItem>();
    }
}
