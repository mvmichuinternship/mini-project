using FoodDeliveryWebApp.exceptions;
using FoodDeliveryWebApp.interfaces;
using FoodDeliveryWebApp.models;
using FoodDeliveryWebApp.models.DTOs.loginDTOs;
using FoodDeliveryWebApp.models.DTOs.registerDTOs;
using FoodDeliveryWebApp.models.errorModel;
using FoodDeliveryWebApp.services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics.CodeAnalysis;

namespace FoodDeliveryWebApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {

        private readonly IRegisterLoginService _registerLoginService;
        private readonly ILogger<UserController> _logger;


        public UserController(IRegisterLoginService registerLoginService, ILogger<UserController> logger)
        {
            _registerLoginService = registerLoginService;
            _logger = logger;
        }

        [HttpPost("RegisterAdmin")]
        [ProducesResponseType(typeof(Admin), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ErrorModel), StatusCodes.Status400BadRequest)]
        [ExcludeFromCodeCoverage]
        public async Task<ActionResult<Admin>> Register(RegisterAdminDTO userDTO)
        {
            if (ModelState.IsValid)
            {

            try
            {
                Admin result = await _registerLoginService.AdminRegister(userDTO);
                return Ok(result);
            }
            catch (UnableToAddException ex)
            {
                _logger.LogCritical("Unable to add admin");
                return BadRequest(new ErrorModel(401, ex.Message));
            }
            }
            return BadRequest("All details are not provided. Please check the object");
        }

        [HttpPost("RegisterCustomer")]
        [ProducesResponseType(typeof(Customer), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ErrorModel), StatusCodes.Status400BadRequest)]
        [ExcludeFromCodeCoverage]
        public async Task<ActionResult<Customer>> Register(RegisterCustomerDTO userDTO)
        {
            if (ModelState.IsValid)
            {

                try
            {
                Customer result = await _registerLoginService.CustomerRegister(userDTO) ;
                return Ok(result);
            }
            catch (UnableToAddException ex)
            {
                _logger.LogCritical("Unable to add Customer");
                return BadRequest(new ErrorModel(401, ex.Message));
            }
            }
            return BadRequest("All details are not provided. Please check the object");
        }

        [HttpPost("Login")]
        [ProducesResponseType(typeof(LoginTokenDTO), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ErrorModel), StatusCodes.Status401Unauthorized)]
        [ExcludeFromCodeCoverage]
        public async Task<ActionResult<LoginTokenDTO>> Login(LoginAdminDTO userLoginDTO)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var result = await _registerLoginService.AdminLogin(userLoginDTO);
                    return Ok(result);
                }
                catch (Exception ex)
                {
                    _logger.LogCritical("User not authenticated");
                    return BadRequest(new ErrorModel(401, ex.Message));
                }
            }
            return BadRequest("All details are not provided. Please check the object");
        }
    }
}
