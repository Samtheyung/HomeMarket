namespace HomeMarket.DTOs.Dashboard
{
    public class MonthlySalesDto
    {
        public int Month { get; set; }

        public string MonthName { get; set; }

        public int Orders { get; set; }

        public decimal Revenue { get; set; }
    }
}
