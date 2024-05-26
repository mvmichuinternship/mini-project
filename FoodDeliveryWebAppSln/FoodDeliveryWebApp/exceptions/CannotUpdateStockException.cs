using System.Runtime.Serialization;

namespace FoodDeliveryWebApp.exceptions
{
    [Serializable]
    internal class CannotUpdateStockException : Exception
    {
        public CannotUpdateStockException()
        {
        }

        public CannotUpdateStockException(string? message) : base(message)
        {
        }

        public CannotUpdateStockException(string? message, Exception? innerException) : base(message, innerException)
        {
        }

        protected CannotUpdateStockException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}