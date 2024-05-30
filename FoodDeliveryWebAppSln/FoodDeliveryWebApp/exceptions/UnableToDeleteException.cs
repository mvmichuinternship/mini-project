using System.Runtime.Serialization;

namespace FoodDeliveryWebApp.exceptions
{
    [Serializable]
    public class UnableToDeleteException : Exception
    {
        string msg;
        public UnableToDeleteException()
        {
        }

        public UnableToDeleteException(string? message) : base(message)
        {
            msg = message;
        }

        public UnableToDeleteException(string? message, Exception? innerException) : base(message, innerException)
        {
        }

        protected UnableToDeleteException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
        public override string Message => msg;

    }
}