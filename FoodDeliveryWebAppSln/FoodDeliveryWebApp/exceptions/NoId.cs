using System.Runtime.Serialization;

namespace FoodDeliveryWebApp.exceptions
{
    [Serializable]
    internal class NoId : Exception
    {
        public NoId()
        {
        }

        public NoId(string? message) : base(message)
        {
        }

        public NoId(string? message, Exception? innerException) : base(message, innerException)
        {
        }

        protected NoId(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}