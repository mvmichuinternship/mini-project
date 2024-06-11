using System.Diagnostics.CodeAnalysis;
using System.Runtime.Serialization;

namespace FoodDeliveryWebApp.exceptions
{
    [Serializable]
    [ExcludeFromCodeCoverage]
    public class UnableToRegisterException : Exception
    {
        string msg;
        public UnableToRegisterException()
        {
        }

        public UnableToRegisterException(string? message) : base(message)
        {
            msg = $"{message}";
        }

        public UnableToRegisterException(string? message, Exception? innerException) : base(message, innerException)
        {
        }

        protected UnableToRegisterException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }

        public override string Message => msg;

    }
}