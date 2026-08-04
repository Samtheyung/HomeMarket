namespace HomeMarket.DTOs.Dashboard
{
    public class TopCustomerDto
    {
        public int CustomerId { get; set; }

        public string FullName { get; set; }

        public string Email { get; set; }

        public int TotalOrders { get; set; }

        public decimal TotalSpent { get; set; }
    }
}
