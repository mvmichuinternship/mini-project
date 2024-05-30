using System.Runtime.Serialization;

namespace FoodDeliveryWebApp.exceptions
{
    [Serializable]
    public class NoId : Exception
    {
        string msg;
        public NoId()
        {
             msg = string.Empty;
        }

        public NoId(string? message) : base(message)
        {
            msg = $"{message}";
        }

        public NoId(string? message, Exception? innerException) : base(message, innerException)
        {
        }

        protected NoId(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
        public override string Message => msg;
    }
}