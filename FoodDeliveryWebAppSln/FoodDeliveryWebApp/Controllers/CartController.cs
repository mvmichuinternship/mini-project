using FoodDeliveryWebApp.exceptions;
using FoodDeliveryWebApp.interfaces;
using FoodDeliveryWebApp.models;
using FoodDeliveryWebApp.models.DTOs.cartDTO;
using FoodDeliveryWebApp.models.errorModel;
using FoodDeliveryWebApp.services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace FoodDeliveryWebApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CartController : ControllerBase
    {

        private readonly ICartService _cartServices;

        public CartController(ICartService cartServices)
        {
            _cartServices = cartServices;
        }


        [Authorize(Roles = "Customer")]
        [Route("RegisterCart")]
        [HttpPost]
        [ProducesResponseType(typeof(CartDetails), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ErrorModel), StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<CartDetails>> Register(CartAndDetailsDTO menu)
        {
            try
            {
                CartDetails result = await _cartServices.AddCartAndDetails(menu);
                return Ok(result);
            }
            catch (UnableToAddException ex)
            {
                return BadRequest(new ErrorModel(401, ex.Message));
            }
        }



        [Authorize(Roles = "Customer")]
        [HttpGet]
        public async Task<Cart> ViewCart(int id)
        {

            var cart = await _cartServices.GetCart(id);
            return cart;


        }
    }
}
