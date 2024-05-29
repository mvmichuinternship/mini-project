using FoodDeliveryWebApp.models;

namespace FoodDeliveryWebApp.interfaces
{
    public interface IFeedbackService
    {
        public Task<Feedback> SendFeedback(Feedback feedback);
        public Task<Feedback> DeleteFeedback(int id);
        public Task<FbComment> AddComment(FbComment comment);
        public Task<FbComment> DeleteComment(int id);

    }
}
