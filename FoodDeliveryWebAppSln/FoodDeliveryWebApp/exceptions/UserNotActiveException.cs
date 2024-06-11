using System.Diagnostics.CodeAnalysis;
using System.Runtime.Serialization;

namespace FoodDeliveryWebApp.exceptions
{
    [Serializable]
    [ExcludeFromCodeCoverage]
    public class UserNotActiveException : Exception
    {
        string msg;
        public UserNotActiveException()
        {
        }

        public UserNotActiveException(string? message) : base(message)
        {
            msg = message;
        }

        public UserNotActiveException(string? message, Exception? innerException) : base(message, innerException)
        {
        }

        protected UserNotActiveException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }

        public override string Message => msg;

    }
}