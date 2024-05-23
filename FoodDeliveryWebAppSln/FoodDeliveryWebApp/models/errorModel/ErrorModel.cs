namespace FoodDeliveryWebApp.models.errorModel
{
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
