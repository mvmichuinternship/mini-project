using System.Diagnostics.CodeAnalysis;
using System.Runtime.Serialization;

namespace FoodDeliveryWebApp.exceptions
{
    [Serializable]
    [ExcludeFromCodeCoverage]
    public class UnauthorizedUserException : Exception
    {
        string msg;
        public UnauthorizedUserException()
        {
        }

        public UnauthorizedUserException(string? message) : base(message)
        {
            msg = message;
        }

        public UnauthorizedUserException(string? message, Exception? innerException) : base(message, innerException)
        {
        }

        protected UnauthorizedUserException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
        public override string Message => msg;

    }
}