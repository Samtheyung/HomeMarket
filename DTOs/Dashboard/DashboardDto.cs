namespace HomeMarket.DTOs.Dashboard
{
    public class DashboardDto
    {
        public int TotalOrders { get; set; }

        public int PendingOrders { get; set; }

        public int DeliveredOrders { get; set; }

        public decimal TotalRevenue { get; set; }
    }
}
