using FoodDeliveryWebApp.exceptions;
using FoodDeliveryWebApp.interfaces;
using FoodDeliveryWebApp.models;
using FoodDeliveryWebApp.models.DTOs.cartDTO;
using FoodDeliveryWebApp.models.errorModel;
using FoodDeliveryWebApp.repositories.dummymodel;
using FoodDeliveryWebApp.services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics.CodeAnalysis;

namespace FoodDeliveryWebApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CartController : ControllerBase
    {

        private readonly ICartService _cartServices;
        private readonly ILogger<UserController> _logger;


        public CartController(ICartService cartServices, ILogger<UserController> logger)
        {
            _cartServices = cartServices;
            _logger = logger;
        }


        //[Authorize(Roles = "Customer")]
        [Route("RegisterCart")]
        [HttpPost]
        [ProducesResponseType(typeof(CartDetails), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ErrorModel), StatusCodes.Status400BadRequest)]
        [ExcludeFromCodeCoverage]

        public async Task<ActionResult<CartDetails>> Register(CartAndDetailsDTO menu)
        {
            if (ModelState.IsValid)
            {

            try
            {
                CartDetails result = await _cartServices.AddCartAndDetails(menu);
                return Ok(result);
            }
            catch (UnableToAddException ex)
            {
                    _logger.LogCritical("Unable to add cart");
                    return BadRequest(new ErrorModel(401, ex.Message));
            }
            }
            return BadRequest("All details are not provided. Please check the object");
        }



        //[Authorize(Roles = "Customer")]
        [HttpGet]
        [ExcludeFromCodeCoverage]

        public async Task<Cart> ViewCart(int id)
        {

            var cart = await _cartServices.GetCart(id);
            return cart;


        }


        //[Authorize(Roles = "Customer")]
        [HttpGet("GetTotal")]
        [ExcludeFromCodeCoverage]

        public async Task<CartTotalResult> ViewTotal(int id)
        {

            var cart = await _cartServices.GetTotal(id);
            return cart;


        }
    }
}
