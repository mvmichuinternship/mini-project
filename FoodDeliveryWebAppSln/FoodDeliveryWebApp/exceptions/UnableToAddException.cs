using System.Diagnostics.CodeAnalysis;
using System.Runtime.Serialization;

namespace FoodDeliveryWebApp.exceptions
{
    [Serializable]
    [ExcludeFromCodeCoverage]
    public class UnableToAddException : Exception
    {
        string msg;
        public UnableToAddException()
        {
        }

        public UnableToAddException(string? message) : base(message)
        {
            msg = message;
        }

        public UnableToAddException(string? message, Exception? innerException) : base(message, innerException)
        {
        }

        protected UnableToAddException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }

        public override string Message => msg;
    }
}