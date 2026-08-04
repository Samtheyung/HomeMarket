using HomeMarket.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace HomeMarket.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomersController : ControllerBase
    {
        private readonly ICustomerService _customerService;

        public CustomersController(
            ICustomerService customerService)
        {
            _customerService = customerService;
        }


        // GET: api/<CustomersController>
        [HttpGet("get-all-customers")]
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }



        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetCustomer(int id)
        {
            var customer = await _customerService.GetCustomerByIdAsync(id);

            if (customer == null)
                return NotFound();

            return Ok(customer);
        }

        [HttpGet("{email}/orders")]
        public async Task<IActionResult> GetCustomerOrders(string email, string name)
        {
            try
            {
                var customer = await _customerService.FindCustomerAsync(email, name);

                if(customer == null)
                {
                    return NotFound($"Customer with email '{email}' not found.");
                }

                return Ok(customer.Orders);

                //return Ok(
                //await _customerService.GetCustomerOrdersAsync(customer.CustomerId));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
            
            

            
        }
    }
}

