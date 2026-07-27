namespace HomeMarket.Models.DbModels
{
    public enum OrderStatus
    {

        Pending,
        Confirmed,
        Preparing,
        ReadyForDelivery,
        OutForDelivery,
        Delivered,
        Cancelled
    }
}

