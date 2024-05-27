using FoodDeliveryWebApp.exceptions;
using FoodDeliveryWebApp.interfaces;
using FoodDeliveryWebApp.models.DTOs.orderDTO;
using FoodDeliveryWebApp.models.errorModel;
using FoodDeliveryWebApp.models;
using FoodDeliveryWebApp.repositories.dummymodel;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace FoodDeliveryWebApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderController : ControllerBase
    {
        private readonly IOrderService _orderServices;

        public OrderController(IOrderService orderServices)
        {
            _orderServices = orderServices;
        }


        //[Authorize(Roles = "Customer")]
        [Route("RegisterOrder")]
        [HttpPost]
        [ProducesResponseType(typeof(string), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ErrorModel), StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<string>> Register(OrderAndDetailsDTO menu)
        {
            try
            {
                string result = await _orderServices.AddOrderAndDetails(menu);
                return Ok(result);
            }
            catch (UnableToAddException ex)
            {
                return BadRequest(new ErrorModel(401, ex.Message));
            }
        }



        //[Authorize(Roles = "Customer")]
        [HttpGet]
        public async Task<Order> ViewOrder(int id)
        {

            var order = await _orderServices.GetOrder(id);
            return order;


        }


        //[Authorize(Roles = "Customer")]
        [HttpGet("GetTotal")]
        public async Task<OrderTotalResult> ViewTotal(int id)
        {

            var order = await _orderServices.GetTotal(id);
            return order;


        }
    }
}
