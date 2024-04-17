using HamroPharma.API.Models.Domains;
using HamroPharma.API.Models.DTO;
using HamroPharma.API.Repositories.Implementation;
using HamroPharma.API.Repositories.Interface;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace HamroPharma.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomerController : ControllerBase
    {
        private readonly IcustomerRepository _customerRepository;

        public CustomerController(IcustomerRepository customerRepository)
        {
            _customerRepository = customerRepository;
        }

        // GET: api/customers
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Customer>>> GetAllCustomers()
        {
            try
            {
                var customers = await _customerRepository.GetAllAysnc();
                return Ok(customers);
            }
            catch
            {
                return StatusCode(500, "Failed to retrieve customers");
            }
        }

        // POST: api/customers
        [HttpPost]
        public async Task<ActionResult<Customer>> AddCustomer(CustomerDto customerDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                var customer = new Customer
                {
                    Id = Guid.NewGuid(),
                    Name = customerDto.Name,
                    Email = customerDto.Email,
                    Phone = customerDto.Phone,
                    PhoneNumber = customerDto.PhoneNumber,
                    Address = customerDto.Address,
                    CustomerBalance = customerDto.CustomerBalance
                };

                var addedCustomer = await _customerRepository.AddAysnc(customer);
                return CreatedAtAction(nameof(GetCustomer), new { id = addedCustomer.Id }, addedCustomer);
            }
            catch
            {
                return StatusCode(500, "Failed to add the customer");
            }
        }

        // GET: api/customers/{id}
        [HttpGet("{id}")]
        public async Task<ActionResult<Customer>> GetCustomer(Guid id)
        {
            var customer = await _customerRepository.GetByIdAysnc(id);

            if (customer == null)
            {
                return NotFound();
            }

            return Ok(customer);
        }

        // PUT: api/customers/{id}
        [HttpPut("{id}")]
        public async Task<IActionResult> EditCustomer(Guid id, CustomerDto customerDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                var existingCustomer = await _customerRepository.GetByIdAysnc(id);

                if (existingCustomer == null)
                {
                    return NotFound("Customer not found");
                }

                existingCustomer.Name = customerDto.Name;
                existingCustomer.Email = customerDto.Email;
                existingCustomer.Phone = customerDto.Phone;
                existingCustomer.PhoneNumber = customerDto.PhoneNumber;
                existingCustomer.Address = customerDto.Address;
                existingCustomer.CustomerBalance = customerDto.CustomerBalance;

                await _customerRepository.UpdateAysnc(existingCustomer);

                return NoContent();
            }
            catch
            {
                return StatusCode(500, "Failed to update the customer");
            }
        }

        // DELETE: api/customers/{id}
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCustomer(Guid id)
        {
            try
            {
                var customer = await _customerRepository.GetByIdAysnc(id);
                if (customer == null)
                {
                    return NotFound(); // Customer not found
                }

                var deletedCustomer = await _customerRepository.DeleteAysnc(customer);
                if (deletedCustomer == null)
                {
                    return NotFound(); // Customer not found
                }

                return NoContent(); // Deletion successful
            }
            catch
            {
                return StatusCode(500, "Failed to delete the customer"); // Deletion failed due to exception
            }
        }
    }
}
