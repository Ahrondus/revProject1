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
    public class OrdersController : ControllerBase
    {
        private readonly ILogger<OrdersController> _logger;
        public OrdersController(ILogger<OrdersController> logger)
        {
            _logger = logger;
        }

        OrdersModel model = new OrdersModel();
        OrderItemsModel OImodel = new OrderItemsModel();

        #region Add Order
        [HttpPost]
        [Route("Add Order")]
        public IActionResult AddOrder(int custId)
        {
            _logger.LogInformation("Adding an Order");
            try
            {
                return Created("", model.AddNewOrder(custId));
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        #endregion

        #region List All Orders
        [HttpGet]
        [Route("List All Orders")]
        public IActionResult ListOrders()
        {
            _logger.LogInformation("Listing All Orders");
            try
            {
                return Ok(model.GetOrderList());
            }
            catch(Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        #endregion

        #region Find Order by Order ID
        [HttpGet]
        [Route("Find Order by Order ID")]
        public IActionResult FindOrder(int id)
        {
            _logger.LogInformation("Finding Order By Order ID");
            try
            {
                return Ok(model.FindAnOrder(id));
            }
            catch (SqlException ex)
            {
                throw new Exception(ex.Message);
            }
        }
        #endregion

        #region Find Order by Customer ID
        [HttpGet]
        [Route("Find Order by Customer ID")]
        public IActionResult FindCustOrder(int id)
        {
            _logger.LogInformation("Finding Order by Customer ID");
            try
            {
                return Ok(model.FindCustomerOrders(id));
            }
            catch (SqlException ex)
            {
                throw new Exception(ex.Message);
            }
        }
        #endregion

        #region Add to Order
        [HttpPut]
        [Route("Add Items to Order")]
        public IActionResult AddToOrder(int OrderID, int ItemID)
        {
            _logger.LogInformation("Adding Items to Order");
            try
            {
                return Ok(OImodel.AddItemsToOrder(OrderID, ItemID));
            }
            catch(SqlException ex)
            {
                throw new Exception(ex.Message);
            }
        }
        #endregion
    }
}
