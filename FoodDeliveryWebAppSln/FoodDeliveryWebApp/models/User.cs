namespace FoodDeliveryWebApp.models
{
    public class User
    {
        public int Id { get; set; }
        public string Role { get; set; }
        public string Password { get; set; }
        public string PasswordHashKey { get; set; }
        public string Phone { get; set; }
        public string Name { get; set; }

        public Customer Customer { get; set; }
        public Admin Admin { get; set; }
    }
}
