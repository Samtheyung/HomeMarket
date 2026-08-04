namespace HomeMarket.DTOs.Dashboard
{
    public class DailySalesDto
    {
        public DateTime Date { get; set; }

        public int Orders { get; set; }

        public decimal Revenue { get; set; }
    }
}
