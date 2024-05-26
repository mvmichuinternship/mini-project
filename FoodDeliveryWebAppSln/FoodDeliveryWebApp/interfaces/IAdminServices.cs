using FoodDeliveryWebApp.models;

namespace FoodDeliveryWebApp.interfaces
{
    public interface IAdminServices
    {
        public Task<Menu> AddFoodToMenu(Menu menu);
        public Task<Menu> UpdateMenu(Menu menu);
        public Task<Menu> DeleteMenu(int key);
        public Task<Menu> UpdateStock(int id, int qty);
        public Task<IEnumerable<Menu>> GetAllMenus();

    }
}
