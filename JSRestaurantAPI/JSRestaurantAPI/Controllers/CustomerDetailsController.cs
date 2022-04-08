using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using JSRestaurantAPI.Models;
using System;
using System.Data.SqlClient;
using Microsoft.Extensions.Logging;

namespace JSRestaurantAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomerDetailsController : ControllerBase
    {
        private readonly ILogger<CustomerDetailsController> _logger;
        public CustomerDetailsController(ILogger<CustomerDetailsController> logger)
        {
            _logger = logger;
        }
        CustomerDetailsModel model = new CustomerDetailsModel();

        [HttpPost]
        [Route("Add Customer")]
        public IActionResult CustomerAdd(CustomerDetailsModel newCustomer)
        {
            _logger.LogInformation("Adding Customer");
            try
            {
                return Created("", model.AddNewCustomer(newCustomer));
            }
            catch(SqlException ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet]
        [Route("Customer List")]
        public IActionResult CustomerList()
        {
            _logger.LogInformation("Listing All Customers");
            try
            {
                return Ok(model.GetCustomerList());
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet]
        [Route("Look up Customer")]
        public IActionResult FindCustomer(int id)
        {
            _logger.LogInformation("Searching for Customer");
            try
            {
                return Ok(model.GetCustomerID(id));
            }
            catch(SqlException ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
