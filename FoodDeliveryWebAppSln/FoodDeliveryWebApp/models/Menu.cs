using System.ComponentModel.DataAnnotations;

namespace FoodDeliveryWebApp.models
{
    public class Menu
    {
        [Key]
        public int FId {  get; set; }
        public string FName { get; set; }
        public int UnitPrice {  get; set; }
        public int QuantityInStock {  get; set; }
    }
}
