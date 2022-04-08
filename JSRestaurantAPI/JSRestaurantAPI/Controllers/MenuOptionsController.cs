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

    
    public class MenuOptionsController : ControllerBase
    {
        private readonly ILogger<MenuOptionsController> _logger;
        public MenuOptionsController(ILogger<MenuOptionsController> logger)
        {
            _logger = logger;
        }

        MenuOptionsModel model = new MenuOptionsModel();

        [HttpGet]
        [Route("Menu List")]
        public IActionResult ProductList(string prodType)
        {
            _logger.LogInformation("Listing Menu");

            if (prodType == "Drinks" || prodType == "Appetizers" || prodType == "Entrees" || prodType == "Desserts" || prodType == null)
                return Ok(model.GetProductList(prodType));
            else
                return BadRequest("Valid options include: Drinks, Appetizers, Entrees, and Deserts");
                
        }

        [HttpPost]
        [Route("Add Menu Option")]
        public IActionResult AddProduct(string name, double price, string category, int quantitiy)
        {
            _logger.LogInformation("Adding Menu Option");
            try
            {
                return Created("", model.AddNewProduct(name, price, category, quantitiy));
            }
            catch (SqlException ex)
            {
                return BadRequest(ex.Message + "\nPlease completely fill all required fields to add a product!");
            }
        }

        [HttpDelete]
        [Route("Delete Menu Option")]
        public IActionResult DeleteProduct(int id)
        {
            _logger.LogInformation("Deleting Menu Option");
            try
            {
                return Accepted(model.GetDeleteProduct(id));
            }
            catch(Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut]
        [Route("Update Menu Option")]
        public IActionResult UpdateProduct(MenuOptionsModel updates)
        {
            _logger.LogInformation("Updating Menu Option");
            try
            {
                return Accepted(model.GetUpdateProduct(updates));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
