using FoodDeliveryWebApp.exceptions;
using FoodDeliveryWebApp.interfaces;
using FoodDeliveryWebApp.models;
using FoodDeliveryWebApp.models.DTOs.loginDTOs;
using FoodDeliveryWebApp.models.DTOs.registerDTOs;
using FoodDeliveryWebApp.models.errorModel;
using FoodDeliveryWebApp.services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace FoodDeliveryWebApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {

        private readonly IRegisterLoginService _registerLoginService;

        public UserController(IRegisterLoginService registerLoginService)
        {
            _registerLoginService = registerLoginService;
        }

        [HttpPost("RegisterAdmin")]
        [ProducesResponseType(typeof(Admin), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ErrorModel), StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<Admin>> Register(RegisterAdminDTO userDTO)
        {
            try
            {
                Admin result = await _registerLoginService.AdminRegister(userDTO);
                return Ok(result);
            }
            catch (UnableToAddException ex)
            {
                return BadRequest(new ErrorModel(501, ex.Message));
            }
        }

        [HttpPost("RegisterCustomer")]
        [ProducesResponseType(typeof(Customer), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ErrorModel), StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<Customer>> Register(RegisterCustomerDTO userDTO)
        {
            try
            {
                Customer result = await _registerLoginService.CustomerRegister(userDTO) ;
                return Ok(result);
            }
            catch (UnableToAddException ex)
            {
                return BadRequest(new ErrorModel(501, ex.Message));
            }
        }

        [HttpPost("Login")]
        [ProducesResponseType(typeof(LoginTokenDTO), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ErrorModel), StatusCodes.Status401Unauthorized)]
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
                    //_logger.LogCritical("User not authenticated");
                    return BadRequest(new ErrorModel(401, ex.Message));
                }
            }
            return BadRequest("All details are not provided. Please check the object");
        }
    }
}
