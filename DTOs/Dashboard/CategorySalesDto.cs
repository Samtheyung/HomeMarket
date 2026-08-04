namespace HomeMarket.DTOs.Dashboard
{
    public class CategorySalesDto
    {
        public int CategoryId { get; set; }

        public string CategoryName { get; set; }

        public int QuantitySold { get; set; }

        public decimal Revenue { get; set; }
    }
}
