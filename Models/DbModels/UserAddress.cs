namespace HomeMarket.Models.DbModels
{
    public class UserAddress
    {
        public UserAddress()
        {
            Country = "South Africa";
        }
        private string streetName { get; set; }
        private string City { get; set; }
        private string Province { get; set; }
        private string postalCode { get; set; }
        private string Country { get; set; }
        private enum BuildingType
        {
            House,
            Apartment,
            Office,
            Other
        }

        private string ? complexName { get; set; }
        private string ?unitNumber { get; set; }

        private string ?blockNumber { get; set; }

    }
}
