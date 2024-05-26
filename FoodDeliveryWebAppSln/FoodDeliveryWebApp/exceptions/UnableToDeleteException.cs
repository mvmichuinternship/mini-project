using System.Runtime.Serialization;

namespace FoodDeliveryWebApp.exceptions
{
    [Serializable]
    internal class UnableToDeleteException : Exception
    {
        public UnableToDeleteException()
        {
        }

        public UnableToDeleteException(string? message) : base(message)
        {
        }

        public UnableToDeleteException(string? message, Exception? innerException) : base(message, innerException)
        {
        }

        protected UnableToDeleteException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}