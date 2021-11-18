using customersService.Entities;
using customersService.Repository.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace customersService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomersController : ControllerBase
    {
        private readonly ICustomerRepository _repository;
        private readonly ILogger<CustomersController> _logger;

        public CustomersController(ICustomerRepository repository, ILogger<CustomersController> logger)
        {
            _repository = repository ?? throw new ArgumentNullException(nameof(repository));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<Customer>), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<IEnumerable<Customer>>> GetCustomers()
        {
            var customers = await _repository.GetCustomers();
            return Ok(customers);
        }

        [HttpGet("{id:length(24)}", Name = "GetCustomer")]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        [ProducesResponseType(typeof(Customer), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<Customer>> GetCustomerById(string id)
        {
            var customer = await _repository.GetCustomer(id);

            if (customer == null)
            {
                _logger.LogError($"Customer with id: {id}, not found.");
                return NotFound();
            }

            return Ok(customer);
        }

        [HttpPost]
        [ProducesResponseType(typeof(Customer), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<Customer>> CreateCustomer([FromBody] Customer customer)
        {
            await _repository.CreateCustomer(customer);

            return CreatedAtRoute("GetCustomer", new { id = customer.Id }, customer);
        }

        [HttpPut]
        [ProducesResponseType(typeof(Customer), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> UpdateCustomer([FromBody] Customer customer)
        {
            return Ok(await _repository.UpdateCustomer(customer));
        }

        [HttpDelete("{id:length(24)}", Name = "DeleteCustomer")]
        [ProducesResponseType(typeof(Customer), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> DeleteCustomerById(string id)
        {
            return Ok(await _repository.DeleteCustomer(id));
        }

    }
}
