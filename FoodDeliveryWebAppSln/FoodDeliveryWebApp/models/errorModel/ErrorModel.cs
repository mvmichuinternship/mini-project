using System.Diagnostics.CodeAnalysis;

namespace FoodDeliveryWebApp.models.errorModel
{
    [ExcludeFromCodeCoverage]
    public class ErrorModel
    {
        int errorCode;
        public string message;

        public ErrorModel(int errorCode, string message)
        {

        }

        public ErrorModel() { }
    }
}
