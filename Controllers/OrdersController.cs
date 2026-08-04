using HomeMarket.DTOs.Order;
using HomeMarket.Models.DbModels;
using HomeMarket.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace HomeMarket.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrdersController : ControllerBase
    {
        // GET: api/<OrdersController>
        private readonly IOrderService _orderService;
        private readonly ILogger<OrdersController> _logger;

        public OrdersController(IOrderService orderService, ILogger<OrdersController> logger)
        {
            _orderService = orderService;
            _logger = logger;
        }

        [HttpPost("place-new-order")]
        public async Task<IActionResult> PlaceOrder(CreateOrderDto dto)
        {
            try
            {
                if (!dto.Customer.Email.Contains("@") || !dto.Customer.Email.Contains(".com"))
                {
                    return BadRequest("Invalid email format. Please provide a valid email address.");
                }
                if (dto.Customer.PhoneNumber.Length < 10 || dto.Customer.PhoneNumber.Length > 10)
                {
                    return BadRequest("Invalid phone number format. Please provide a valid phone number.");
                }
                var order = await _orderService.PlaceOrderAsync(dto);

                return Ok(order);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error placing order");
                return StatusCode(500, "An error occurred while placing the order.");
            }

        }

        [HttpGet]
        public async Task<IActionResult> GetOrders()
        {
            return Ok(await _orderService.GetAllOrdersAsync());
        }

        [HttpGet("get-order-by-status/{id:int}")]
        public async Task<IActionResult> GetOrder(int id)
        {
            var order = await _orderService.GetOrderAsync(id);

            if (order == null)
                return NotFound();

            return Ok(order);
        }

        [HttpGet("get-status-by-status/{status}")]
        public async Task<IActionResult> GetOrdersByStatus(
            OrderStatus status)
        {
            return Ok(
                await _orderService.GetOrdersByStatusAsync(status));
        }

        [HttpPatch("{id:int}/update-status-by-id")]
        public async Task<IActionResult> UpdateStatus(
            int id,
            [FromBody] OrderStatus status)
        {
            await _orderService.UpdateStatusAsync(id, status);

            return NoContent();
        }

        [HttpDelete("{id:int}")]
        public async Task<IActionResult> CancelOrder(int id)
        {
            await _orderService.CancelOrderAsync(id);

            return NoContent();
        }
    }
}
