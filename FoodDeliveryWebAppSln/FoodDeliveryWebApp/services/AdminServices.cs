using FoodDeliveryWebApp.exceptions;
using FoodDeliveryWebApp.interfaces;
using FoodDeliveryWebApp.models;
using Menu = FoodDeliveryWebApp.models.Menu;

namespace FoodDeliveryWebApp.services
{
    public class AdminServices : IAdminServices
    {
        private readonly IRepository<int, Menu> _menuRepository;

        public AdminServices(IRepository<int, Menu> menuRepository)
        {
            _menuRepository = menuRepository;
        }
        public async Task<Menu> AddFoodToMenu(Menu menu)
        {
            var result = await  _menuRepository.Add(menu);
            return result;
        }

        public async Task<Menu> DeleteMenu(int menu)
        {
            var result = await _menuRepository.Delete(menu);
            return result;
        }

        public async Task<IEnumerable<Menu>> GetAllMenus()
        {
            var result = await _menuRepository.GetAll();
            return result;
        }

        public async Task<Menu> UpdateMenu(Menu menuitem)
        {
            var menu = await _menuRepository.Get(menuitem.FId);
            if (menu == null)
                throw new Exception();
            menu.UnitPrice = menuitem.UnitPrice;
            menu.QuantityInStock = menuitem.QuantityInStock;
            menu = await _menuRepository.Update(menu);
            return menu;
        }

        public async Task<Menu> UpdateStock(int id, int qty)
        {
            var result = await _menuRepository.Get(id);
            if (result.QuantityInStock != 0)
            {
                result.QuantityInStock -= qty;
                result = await _menuRepository.Update(result);
            }
            else
            {
                throw new CannotUpdateStockException("Quantity cannot beyond 0");
            }
            return result;
            
        }
    }
}
