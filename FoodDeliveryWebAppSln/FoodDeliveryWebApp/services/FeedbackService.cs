using FoodDeliveryWebApp.exceptions;
using FoodDeliveryWebApp.interfaces;
using FoodDeliveryWebApp.Migrations;
using FoodDeliveryWebApp.models;
using FoodDeliveryWebApp.repositories;

namespace FoodDeliveryWebApp.services
{
    public class FeedbackService : IFeedbackService
    {
        private readonly IRepository<int,Feedback> _feedbackRepository;
        private readonly IRepository<int,FbComment> _fbCommentRepository;
        private readonly FeedbackCommentRepository _feedbackCommentRepository;

        public FeedbackService( IRepository<int, Feedback> feedbackRepository, IRepository<int, FbComment> fbCommentRepository, FeedbackCommentRepository feedbackCommentRepository)
        {
            _fbCommentRepository = fbCommentRepository;
            _feedbackRepository = feedbackRepository;
            _feedbackCommentRepository = feedbackCommentRepository;
        }

        public async Task<FbComment> AddComment(FbComment comment)
        {
            FbComment fbComment = await _fbCommentRepository.Add(comment);
            if(fbComment != null)
            {
                return fbComment;
            }
            else
            {
                throw new UnableToAddException("Unable to reply to feedback");
            }
        }

        public async Task<FbComment> DeleteComment(int id)
        {
            FbComment fbCommentid = await _feedbackCommentRepository.GetByFbId(id);

            if (fbCommentid == null)
            {
                FbComment fbc = await _fbCommentRepository.Delete(fbCommentid.FbId);
                return fbc;
            }
            else
            {
                throw new UnableToDeleteException("Unable to delete reply to feedback");
            }
        }

        public async Task<Feedback> DeleteFeedback(int id)
        {
            Feedback fb = await _feedbackRepository.Delete(id);
            if (fb != null)
            {

                return fb;
            }
            else
            {
                throw new UnableToDeleteException("Unable to delete feedback");
            }
        }

        public async Task<Feedback> SendFeedback(Feedback feedback)
        {
            Feedback fb = await _feedbackRepository.Add(feedback);
            if(fb != null)
            {
                return fb;
            }
            else
            {
                throw new UnableToAddException("Unable to send feedback");
            }
        }
    }
}
