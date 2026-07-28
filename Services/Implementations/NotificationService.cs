using HomeMarket.Models.DbModels;
using HomeMarket.Services.Interfaces;
using System.Net;
using System.Net.Mail;

namespace HomeMarket.Services.Implementations
{
    public class NotificationService : INotificationService
    {
        private readonly IConfiguration _configuration;


        public NotificationService(
            IConfiguration configuration)
        {
            _configuration = configuration;
        }


        public async Task SendOrderConfirmationAsync(
            Order order)
        {
            var subject =
                $"Order Confirmation #{order.OrderId}";


            var body =
            $@"
        Hello {order.Customer.FirstName},

        Thank you for your order.

        Order Number: {order.OrderId}

        Total Amount: {order.TotalAmount:C}

        Payment Method: {order.PaymentMethod}

        We will contact you shortly.

        Thank you.
        ";


            await SendEmailAsync(
                order.Customer.Email,
                subject,
                body);
        }



        public async Task SendNewOrderNotificationAsync(
            Order order)
        {
            var subject =
                $"New Order Received #{order.OrderId}";


            var body =
            $@"
        A new order has been received.

        Customer:
        {order.Customer.FirstName}
        {order.Customer.LastName}

        Phone:
        {order.Customer.PhoneNumber}

        Address:
        {order.Customer.DeliveryAddress}

        Total:
        {order.TotalAmount:C}
        ";


            await SendEmailAsync(
                "owner@yourbusiness.com",
                subject,
                body);
        }



        public async Task SendOrderStatusUpdateAsync(
            Order order)
        {
            var subject =
                $"Order Update #{order.OrderId}";


            var body =
            $@"
        Hello {order.Customer.FirstName},

        Your order status has changed.

        Current Status:
        {order.Status}
        ";


            await SendEmailAsync(
                order.Customer.Email,
                subject,
                body);
        }



        private async Task SendEmailAsync(
            string email,
            string subject,
            string body)
        {

            var smtp =
                new SmtpClient(
                    _configuration["Email:SmtpServer"])
                {
                    Port = 587,
                    Credentials =
                    new NetworkCredential(
                        _configuration["Email:Username"],
                        _configuration["Email:Password"]),

                    EnableSsl = true
                };


            var message =
                new MailMessage(
                    _configuration["Email:From"],
                    email,
                    subject,
                    body);


            await smtp.SendMailAsync(message);
        }
    }
}
