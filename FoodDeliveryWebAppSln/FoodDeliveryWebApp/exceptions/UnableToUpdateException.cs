using System.Runtime.Serialization;

namespace FoodDeliveryWebApp.exceptions
{
    [Serializable]
    public class UnableToUpdateException : Exception
    {
        string msg;
        public UnableToUpdateException()
        {
        }

        public UnableToUpdateException(string? message) : base(message)
        {
            msg = message;
        }

        public UnableToUpdateException(string? message, Exception? innerException) : base(message, innerException)
        {
        }

        protected UnableToUpdateException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }

        public override string Message => msg;

    }
}