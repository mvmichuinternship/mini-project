using FoodDeliveryWebApp.exceptions;
using FoodDeliveryWebApp.interfaces;
using FoodDeliveryWebApp.models.DTOs.orderDTO;
using FoodDeliveryWebApp.models.errorModel;
using FoodDeliveryWebApp.models;
using FoodDeliveryWebApp.repositories.dummymodel;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics.CodeAnalysis;

namespace FoodDeliveryWebApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderController : ControllerBase
    {
        private readonly IOrderService _orderServices;
        private readonly ILogger<UserController> _logger;


        public OrderController(IOrderService orderServices, ILogger<UserController> logger)
        {
            _orderServices = orderServices;
            _logger = logger;
        }


        //[Authorize(Roles = "Customer")]
        [Route("RegisterOrder")]
        [HttpPost]
        [ProducesResponseType(typeof(string), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ErrorModel), StatusCodes.Status400BadRequest)]
        [ExcludeFromCodeCoverage]

        public async Task<ActionResult<string>> Register(OrderAndDetailsDTO menu)
        {
            if (ModelState.IsValid)
            {

            try
            {
                string result = await _orderServices.AddOrderAndDetails(menu);
                return Ok(result);
            }
            catch (UnableToAddException ex)
            {
                    _logger.LogCritical("Unable to place order");
                    return BadRequest(new ErrorModel(401, ex.Message));
            }
            }
            return BadRequest("All details are not provided. Please check the object");
        }



        //[Authorize(Roles = "Customer")]
        [HttpGet]
        [ExcludeFromCodeCoverage]

        public async Task<Order> ViewOrder(int id)
        {

            var order = await _orderServices.GetOrder(id);
            return order;


        }


        //[Authorize(Roles = "Customer")]
        [HttpGet("GetTotal")]
        [ExcludeFromCodeCoverage]

        public async Task<OrderTotalResult> ViewTotal(int id)
        {

            var order = await _orderServices.GetTotal(id);
            return order;


        }
    }
}
