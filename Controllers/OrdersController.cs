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

        [HttpPost]
        public async Task<IActionResult> PlaceOrder(CreateOrderDto dto)
        {
            var order = await _orderService.PlaceOrderAsync(dto);

            return Ok(order);
        }

        [HttpGet]
        public async Task<IActionResult> GetOrders()
        {
            return Ok(await _orderService.GetAllOrdersAsync());
        }

        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetOrder(int id)
        {
            var order = await _orderService.GetOrderAsync(id);

            if (order == null)
                return NotFound();

            return Ok(order);
        }

        [HttpGet("status/{status}")]
        public async Task<IActionResult> GetOrdersByStatus(
            OrderStatus status)
        {
            return Ok(
                await _orderService.GetOrdersByStatusAsync(status));
        }

        [HttpPatch("{id:int}/status")]
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
