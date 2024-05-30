using FoodDeliveryWebApp.exceptions;
using FoodDeliveryWebApp.interfaces;
using FoodDeliveryWebApp.models;
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
    public class FeedbackController : ControllerBase
    {

        private readonly IFeedbackService _feedbackService;
        private readonly ILogger<UserController> _logger;


        public FeedbackController(IFeedbackService feedbackService, ILogger<UserController> logger)
        {
            _feedbackService = feedbackService;
            _logger = logger;
        }

        [HttpPost("SendFeedback")]
        [ProducesResponseType(typeof(Feedback), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ErrorModel), StatusCodes.Status400BadRequest)]
        [ExcludeFromCodeCoverage]

        public async Task<ActionResult<Feedback>> Register(Feedback fb)
        {
            if (ModelState.IsValid)
            {

            try
            {
                Feedback result = await _feedbackService.SendFeedback(fb);
                return Ok(result);
            }
            catch (UnableToAddException ex)
            {
                    _logger.LogCritical("Unable to send feedback");
                    return BadRequest(new ErrorModel(401, ex.Message));
            }
            }
            return BadRequest("All details are not provided. Please check the object");
        }



        [HttpPost("SendFeedbackComment")]
        [ProducesResponseType(typeof(FbComment), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ErrorModel), StatusCodes.Status400BadRequest)]
        [ExcludeFromCodeCoverage]

        public async Task<ActionResult<FbComment>> AddFbComment(FbComment fb)
        {
            if (ModelState.IsValid)
            {

            try
            {
                FbComment result = await _feedbackService.AddComment(fb);
                return Ok(result);
            }
            catch (UnableToAddException ex)
            {
                    _logger.LogCritical("Unable to reply to feedback");
                    return BadRequest(new ErrorModel(401, ex.Message));
            }
            }
            return BadRequest("All details are not provided. Please check the object");
        }



        [HttpDelete("DeleteFeedback")]
        [ProducesResponseType(typeof(Feedback), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ErrorModel), StatusCodes.Status400BadRequest)]
        [ExcludeFromCodeCoverage]

        public async Task<ActionResult<Feedback>> Delete(int id)
        {
            try
            {
                var result = await _feedbackService.DeleteFeedback(id);
                return Ok(result);
            }
            catch (UnableToDeleteException ex)
            {
                _logger.LogCritical("Unable to delete feedback");
                return BadRequest(new ErrorModel(401, ex.Message));
            }
        }


        [HttpDelete("DeleteFeedbackComment")]
        [ProducesResponseType(typeof(FbComment), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ErrorModel), StatusCodes.Status400BadRequest)]
        [ExcludeFromCodeCoverage]

        public async Task<ActionResult<FbComment>> DeleteCommentFb(int id)
        {
            try
            {
                var result = await _feedbackService.DeleteComment(id);
                return Ok(result);
            }
            catch (UnableToDeleteException ex)
            {
                _logger.LogCritical("Unable to delete reply");
                return BadRequest(new ErrorModel(401, ex.Message));
            }
        }
    }
}
