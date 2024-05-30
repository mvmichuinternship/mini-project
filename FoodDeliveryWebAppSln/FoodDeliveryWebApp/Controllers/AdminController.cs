using FoodDeliveryWebApp.exceptions;
using FoodDeliveryWebApp.interfaces;
using FoodDeliveryWebApp.models;
using FoodDeliveryWebApp.models.DTOs.registerDTOs;
using FoodDeliveryWebApp.models.errorModel;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics.CodeAnalysis;

namespace FoodDeliveryWebApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AdminController : ControllerBase
    {

        private readonly IAdminServices _adminServices;
        private readonly ILogger<UserController> _logger;


        public AdminController(IAdminServices adminServices, ILogger<UserController> logger)
        {
            _adminServices = adminServices;
            _logger = logger;
        }

        //[Authorize(Roles = "Admin")]
        [Route("RegisterMenu")]
        [HttpPost]
        [ProducesResponseType(typeof(Menu), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ErrorModel), StatusCodes.Status400BadRequest)]
        [ExcludeFromCodeCoverage]
        public async Task<ActionResult<Menu>> Register(Menu menu)
        {
            if (ModelState.IsValid)
            {

            try
            {
                Menu result = await _adminServices.AddFoodToMenu(menu);
                return Ok(result);
            }
            catch (UnableToAddException ex)
            {
                    _logger.LogCritical("Unable to add menu");
                    return BadRequest(new ErrorModel(401, ex.Message));
            }
            }
            return BadRequest("All details are not provided. Please check the object");
        }

        //[Authorize(Roles = "Admin")]
        [HttpPut("UpdateMenu")]
        [ProducesResponseType(typeof(Menu), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ErrorModel), StatusCodes.Status400BadRequest)]
        [ExcludeFromCodeCoverage]

        public async Task<ActionResult<Menu>> Update(Menu menu)
        {
            try
            {
                Menu result = await _adminServices.UpdateMenu(menu);
                return Ok(result);
            }
            catch (UnableToUpdateException ex)
            {
                _logger.LogCritical("Unable to update menu");
                return BadRequest(new ErrorModel(401, ex.Message));
            }
        }

        //[Authorize(Roles = "Admin")]
        [HttpDelete("DeleteMenu")]
        [ProducesResponseType(typeof(Menu), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ErrorModel), StatusCodes.Status400BadRequest)]
        [ExcludeFromCodeCoverage]

        public async Task<ActionResult<Menu>> Delete(int menu)
        {
            try
            {
                Menu result = await _adminServices.DeleteMenu(menu);
                return Ok(result);
            }
            catch (UnableToDeleteException ex)
            {
                _logger.LogCritical("Unable to delete menu");
                return BadRequest(new ErrorModel(401, ex.Message));
            }
        }

        //[Authorize(Roles = "Admin")]
        [HttpGet]
        [ExcludeFromCodeCoverage]
        public async Task<IEnumerable<Menu>> ViewRequestAdmin()
        {

            var requestRaise = await _adminServices.GetAllMenus();
            return requestRaise.ToList();


        }
    }
}
