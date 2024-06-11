using FoodDeliveryWebApp.exceptions;
using FoodDeliveryWebApp.interfaces;
using FoodDeliveryWebApp.models;
using FoodDeliveryWebApp.models.DTOs.paymentDTO;
using FoodDeliveryWebApp.models.errorModel;
using FoodDeliveryWebApp.services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics.CodeAnalysis;

namespace FoodDeliveryWebApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PaymentController : ControllerBase
    {
        private readonly IPaymentService _paymentService;
        private readonly ILogger<PaymentController> _logger;


        public PaymentController(IPaymentService paymentService, ILogger<PaymentController> logger)
        {
            _paymentService = paymentService;
            _logger = logger;
        }

        //[Authorize(Roles ="Customer")]
        [Route("MakePayment")]
        [HttpPost]
        [ProducesResponseType(typeof(Payment), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ErrorModel), StatusCodes.Status400BadRequest)]
        [ExcludeFromCodeCoverage]

        public async Task<ActionResult<Payment>> Pay(PaymentDTO pay)
        {
            if (ModelState.IsValid)
            {

            try
            {
                Payment result = await _paymentService.AddPayment(pay);
                return Ok(result);
            }
            catch (UnableToAddException ex)
            {
                    _logger.LogCritical("Unable to send paymentr");
                    return BadRequest(new ErrorModel(401, ex.Message));
            }
            }
            return BadRequest("All details are not provided. Please check the object");
        }


        [HttpGet("ViewPaymentStatus")]
        [ExcludeFromCodeCoverage]

        public async Task<Payment> ViewPayment(int id)
        {

            var order = await _paymentService.ViewPayment(id);
            return order;


        }

        [HttpGet("CancelPayment")]
        [ExcludeFromCodeCoverage]

        public async Task<Payment> CancelPayment(int id)
        {

            var order = await _paymentService.CancelPayment(id);
            return order;


        }
    }
}
