namespace HomeMarket.DTOs.Dashboard
{
    public class DashboardDto
    {
        public int TotalOrders { get; set; }

        public int PendingOrders { get; set; }

        public int DeliveredOrders { get; set; }
        public int TotalCustomers { get; set; }
        public int TotalProducts { get; set; }

        public decimal TotalRevenue { get; set; }
    }
}
