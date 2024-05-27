using FoodDeliveryWebApp.exceptions;
using FoodDeliveryWebApp.interfaces;
using FoodDeliveryWebApp.models;
using FoodDeliveryWebApp.models.DTOs.paymentDTO;
using FoodDeliveryWebApp.models.errorModel;
using FoodDeliveryWebApp.services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace FoodDeliveryWebApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PaymentController : ControllerBase
    {
        private readonly IPaymentService _paymentService;

        public PaymentController(IPaymentService paymentService)
        {
            _paymentService = paymentService;
        }

        //[Authorize(Roles ="Customer")]
        [Route("MakePayment")]
        [HttpPost]
        [ProducesResponseType(typeof(Payment), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ErrorModel), StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<Payment>> Pay(PaymentDTO pay)
        {
            try
            {
                Payment result = await _paymentService.AddPayment(pay);
                return Ok(result);
            }
            catch (UnableToAddException ex)
            {
                return BadRequest(new ErrorModel(401, ex.Message));
            }
        }


        [HttpGet("ViewPaymentStatus")]
        public async Task<Payment> ViewPayment(int id)
        {

            var order = await _paymentService.ViewPayment(id);
            return order;


        }

        [HttpGet("CancelPayment")]
        public async Task<Payment> CancelPayment(int id)
        {

            var order = await _paymentService.CancelPayment(id);
            return order;


        }
    }
}
