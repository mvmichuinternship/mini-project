using FoodDeliveryWebApp.exceptions;
using FoodDeliveryWebApp.interfaces;
using FoodDeliveryWebApp.models;
using FoodDeliveryWebApp.models.DTOs.registerDTOs;
using FoodDeliveryWebApp.models.errorModel;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace FoodDeliveryWebApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AdminController : ControllerBase
    {

        private readonly IAdminServices _adminServices;

        public AdminController(IAdminServices adminServices)
        {
            _adminServices = adminServices;
        }

        //[Authorize(Roles = "Admin")]
        [Route("RegisterMenu")]
        [HttpPost]
        [ProducesResponseType(typeof(Menu), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ErrorModel), StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<Menu>> Register(Menu menu)
        {
            try
            {
                Menu result = await _adminServices.AddFoodToMenu(menu);
                return Ok(result);
            }
            catch (UnableToAddException ex)
            {
                return BadRequest(new ErrorModel(401, ex.Message));
            }
        }

        //[Authorize(Roles = "Admin")]
        [HttpPut("UpdateMenu")]
        [ProducesResponseType(typeof(Menu), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ErrorModel), StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<Menu>> Update(Menu menu)
        {
            try
            {
                Menu result = await _adminServices.UpdateMenu(menu);
                return Ok(result);
            }
            catch (UnableToUpdateException ex)
            {
                return BadRequest(new ErrorModel(401, ex.Message));
            }
        }

        //[Authorize(Roles = "Admin")]
        [HttpDelete("DeleteMenu")]
        [ProducesResponseType(typeof(Menu), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ErrorModel), StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<Menu>> Delete(int menu)
        {
            try
            {
                Menu result = await _adminServices.DeleteMenu(menu);
                return Ok(result);
            }
            catch (UnableToDeleteException ex)
            {
                return BadRequest(new ErrorModel(401, ex.Message));
            }
        }

        //[Authorize(Roles = "Admin")]
        [HttpGet]
        public async Task<IEnumerable<Menu>> ViewRequestAdmin()
        {

            var requestRaise = await _adminServices.GetAllMenus();
            return requestRaise.ToList();


        }
    }
}
