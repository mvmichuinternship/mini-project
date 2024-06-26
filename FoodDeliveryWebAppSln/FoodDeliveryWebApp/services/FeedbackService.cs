using FoodDeliveryWebApp.exceptions;
using FoodDeliveryWebApp.interfaces;
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
            try
            {

            FbComment fbComment = await _fbCommentRepository.Add(comment);
                Feedback fb = await _feedbackRepository.Get(comment.FbId);
                fb.CommentId= fbComment.CommentId;
                var res = await _feedbackRepository.Update(fb);
            
                return fbComment;
            
            }
            catch  
            {
                throw new UnableToAddException("Unable to reply to feedback");
            }
        }

        public async Task<FbComment> DeleteComment(int id)
        {
            try
            {

                FbComment fbCommentid = await _feedbackCommentRepository.GetByFbId(id);

                //if (fbCommentid == null)
                //{
                Feedback feedbackCommentUpdate = await _feedbackRepository.Get(id);
                feedbackCommentUpdate.CommentId = null;
                Feedback fb = await _feedbackRepository.Update(feedbackCommentUpdate);
                FbComment fbc = await _fbCommentRepository.Delete(fbCommentid.CommentId);
                return fbc;
            //}
            }
            catch
            {
                throw new UnableToDeleteException("Unable to delete reply to feedback");
            }
        }

        public async Task<Feedback> DeleteFeedback(int id)
        {
            try
            {

            FbComment commentId =  await _feedbackCommentRepository.GetByFbId(id);
            FbComment fbComment = await _feedbackCommentRepository.Delete(commentId.CommentId);
            Feedback fb = await _feedbackRepository.Delete(id);
            

                return fb;
            
            }
            catch
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
