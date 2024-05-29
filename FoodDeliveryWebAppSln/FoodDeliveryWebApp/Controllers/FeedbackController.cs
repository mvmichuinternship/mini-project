using FoodDeliveryWebApp.exceptions;
using FoodDeliveryWebApp.interfaces;
using FoodDeliveryWebApp.models;
using FoodDeliveryWebApp.models.DTOs.registerDTOs;
using FoodDeliveryWebApp.models.errorModel;
using FoodDeliveryWebApp.services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace FoodDeliveryWebApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FeedbackController : ControllerBase
    {

        private readonly IFeedbackService _feedbackService;

        public FeedbackController(IFeedbackService feedbackService)
        {
            _feedbackService = feedbackService;
        }

        [HttpPost("SendFeedback")]
        [ProducesResponseType(typeof(Feedback), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ErrorModel), StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<Feedback>> Register(Feedback fb)
        {
            try
            {
                Feedback result = await _feedbackService.SendFeedback(fb);
                return Ok(result);
            }
            catch (UnableToAddException ex)
            {
                return BadRequest(new ErrorModel(401, ex.Message));
            }
        }



        [HttpPost("SendFeedbackComment")]
        [ProducesResponseType(typeof(FbComment), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ErrorModel), StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<FbComment>> AddFbComment(FbComment fb)
        {
            try
            {
                FbComment result = await _feedbackService.AddComment(fb);
                return Ok(result);
            }
            catch (UnableToAddException ex)
            {
                return BadRequest(new ErrorModel(401, ex.Message));
            }
        }



        [HttpDelete("DeleteFeedback")]
        [ProducesResponseType(typeof(Feedback), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ErrorModel), StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<Feedback>> Delete(int id)
        {
            try
            {
                var result = await _feedbackService.DeleteFeedback(id);
                return Ok(result);
            }
            catch (UnableToDeleteException ex)
            {
                return BadRequest(new ErrorModel(401, ex.Message));
            }
        }


        [HttpDelete("DeleteFeedbackComment")]
        [ProducesResponseType(typeof(FbComment), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ErrorModel), StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<FbComment>> DeleteCommentFb(int id)
        {
            try
            {
                var result = await _feedbackService.DeleteComment(id);
                return Ok(result);
            }
            catch (UnableToDeleteException ex)
            {
                return BadRequest(new ErrorModel(401, ex.Message));
            }
        }
    }
}
